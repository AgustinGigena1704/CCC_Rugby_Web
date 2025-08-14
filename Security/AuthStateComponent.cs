using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static CCC_Rugby_Web.Services.Constants;

namespace CCC_Rugby_Web.Security
{
    public class AuthStateComponent
    {
        private readonly string jwtKey;
        private readonly CookieService cookieService;
        private readonly string tokenCookieName = TokenCookieName;


        public AuthStateComponent(CookieService cookieService, IConfiguration configuration)
        {
            this.cookieService = cookieService;
            this.jwtKey = configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT:Key not found in configuration");
        }

        public async Task<string> Auth(Usuario user, List<Role>? roles)
        {
            DateTime vencimiento = DateTime.UtcNow.AddHours(1);
            var token = GenerateJwt(user, roles, vencimiento);
            await cookieService.SetCookieAsync(tokenCookieName, token, vencimiento);
            return token;
        }

        private string GenerateJwt(Usuario user, List<Role>? roles, DateTime vencimiento)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);



            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };
            if (roles != null)
            {
                var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role.Codigo)).ToList();
                claims.AddRange(roleClaims);
            }
            var token = new JwtSecurityToken(
                issuer: "CCC_Rugby_Web",
                audience: "CCC_Rugby_Web_Audience",
                claims: claims,
                expires: vencimiento,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task DeleteTokenFromCookieAsync()
        {
            await cookieService.DeleteCookieAsync(tokenCookieName);
        }

        public async Task<IEnumerable<Claim>>? VerifyUser()
        {
            var token = await cookieService.GetCookieAsync(tokenCookieName);
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            return VerifyUser(token);
        }

        public IEnumerable<Claim> VerifyUser(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "CCC_Rugby_Web",
                ValidAudience = "CCC_Rugby_Web_Audience",
                IssuerSigningKey = securityKey
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jsonToken != null)
                {
                    return jsonToken.Claims.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating token: {ex.Message}");
            }

            return new List<Claim>();
        }
    }
}
