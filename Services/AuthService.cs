using CCC_Rugby_Web.DTOs;
using CCC_Rugby_Web.Models;
using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Models.Repositories;
using CCC_Rugby_Web.Security;

namespace CCC_Rugby_Web.Services
{
    public class AuthService
    {
        private readonly AuthStateProvider authStateProvider;
        private readonly UsuarioRepository usuarioRepository;
        public AuthService(AuthStateProvider authStateProvider, UsuarioRepository usuarioRepository)
        {
            this.authStateProvider = authStateProvider;
            this.usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario?> LoginAsync(LoginDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                throw new ArgumentException("El nombre de usuario o la contraseña no pueden estar vacios.");
            }
            
            var user = await usuarioRepository.GetByPassAndUser(dto.Username, dto.Password);
            if (user == null)
            {
                throw new Exception("Credenciales enviadas invalidas");
            }
            return user;
        }

        public async Task LogOutAsync()
        {
            await authStateProvider.LogoutUser();
        }
    }
}
