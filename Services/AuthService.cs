using CCC_Rugby_Web.DTOs;
using CCC_Rugby_Web.Models;
using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Models.Repositories;
using CCC_Rugby_Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CCC_Rugby_Web.Services
{
    public class AuthService
    {
        private readonly AuthStateProvider authStateProvider;
        private readonly EntityManager entityManager;
        public AuthService(AuthStateProvider authStateProvider, EntityManager entityManager)
        {
            this.authStateProvider = authStateProvider;
            this.entityManager = entityManager;
        }

        public async Task<AuthenticationState?> GetAuthState()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            return authState;
        }

        public async Task<ClaimsPrincipal?> GetAuthUserAsync()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();
            return authState.User.Identity != null && authState.User.Identity.IsAuthenticated ? authState.User : null;
        }

        public async Task<Usuario?> LoginAsync(LoginDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                throw new ArgumentException("El nombre de usuario o la contraseña no pueden estar vacios.");
            }
            var usuarioRepository = entityManager.GetRepository<UsuarioRepository>();
            var user = await usuarioRepository.GetByPassAndUser(dto.Username, dto.Password);
            if (user == null)
            {
                throw new Exception("Credenciales invalidas.");
            }
            user.LastLogin = DateTime.Now;
            await usuarioRepository.UpdateAsync(user);
            return user;
        }

        public async Task LogOutAsync()
        {
            await authStateProvider.LogoutUser();
        }
    }
}
