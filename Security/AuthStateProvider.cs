using CCC_Rugby_Web.Models;
using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Models.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CCC_Rugby_Web.Security
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly UsuarioRepository usuarioRepository;
        private readonly AuthStateComponent authStateComponent;
        private readonly ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        public AuthStateProvider(AuthStateComponent authStateComponent, UsuarioRepository _usuarioRepository)
        {
            this.authStateComponent = authStateComponent;
            this.usuarioRepository = _usuarioRepository;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userClaim = await authStateComponent.VerifyUser();
                if(userClaim != null && userClaim.Count() > 2)
                {
                    var identity = new ClaimsIdentity(userClaim, "JWT");
                    var user = new ClaimsPrincipal(identity);
                    return await Task.FromResult(new AuthenticationState(user));
                }
                else
                {
                    await authStateComponent.DeleteTokenFromCookieAsync();
                    return await Task.FromResult(new AuthenticationState(anonymous));
                }
            } 
            catch(Exception ex)
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async Task AuthenticateUser(Usuario _user)
        {
            List<Rol>? roles = await usuarioRepository.GetRoles(_user.Id);
            var token = await authStateComponent.Auth(_user, roles);

            if (!string.IsNullOrEmpty(token))
            {
                var readJwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity(readJwt.Claims, "JWT");
                var user = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
            }
            else
            {
                await authStateComponent.DeleteTokenFromCookieAsync();
                var authState = new AuthenticationState(anonymous);
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
            }
        }

        public async Task LogoutUser()
        {
            await authStateComponent.DeleteTokenFromCookieAsync();
            var authState = new AuthenticationState(anonymous);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }
    }
}
