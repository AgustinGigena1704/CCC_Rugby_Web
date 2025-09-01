using BCrypt.Net;
using CCC_Rugby_Web.DTOs;
using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace CCC_Rugby_Web.Models.Repositories
{
    [Repository(typeof(Usuario))]
    public class UsuarioRepository : GenericRepository<Usuario>
    {
        public UsuarioRepository(CCC_DbContext context, EntityManager entity, IUserContextService userContextService) : base(context, entity, userContextService) { }
        public async Task<Usuario?> GetByPassAndUser(string username, string password)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Usuario? user;

            try
            {
                if (Regex.IsMatch(username, patron))
                {
                    user = await context.Usuarios
                        .Include(u => u.Persona)
                        .Include(u => u.AvatarArchivo)
                        .Include(u => u.Roles)
                        .FirstOrDefaultAsync(u => u.Email == username && !u.BorradoLogico);
                }
                else
                {
                    user = await context.Usuarios
                        .Include(u => u.Persona)
                        .Include(u => u.AvatarArchivo)
                        .Include(u => u.Roles)
                        .FirstOrDefaultAsync(u => u.Username == username && !u.BorradoLogico);
                }

                if (user != null)
                {
                    // Verificar la contraseña con manejo de errores
                    bool isValidPassword = false;

                    try
                    {
                        isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
                    }
                    catch (Exception ex)
                    {
                        // Log del error de BCrypt
                        Console.WriteLine($"Error verificando contraseña con BCrypt: {ex.Message}");

                        // Fallback: verificar si la contraseña está en texto plano (para migración)
                        if (user.Password == password)
                        {
                            // Actualizar a hash BCrypt
                            user.Password = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
                            context.Usuarios.Update(user);
                            await context.SaveChangesAsync();
                            isValidPassword = true;
                        }
                    }

                    if (isValidPassword)
                    {
                        user.LastLogin = DateTime.UtcNow;
                        user.LoginTrys = 0; // Reset intentos fallidos
                        await context.SaveChangesAsync();
                        return user;
                    }
                    else
                    {
                        // Incrementar intentos fallidos
                        user.LoginTrys++;
                        if (user.LoginTrys >= 5)
                        {
                            user.Bloqueado = true;
                        }
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetByPassAndUser: {ex.Message}");
            }

            return null;
        }

        
        public async Task<Usuario> CreateUserAsync(Usuario user, string plainPassword)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(plainPassword, BCrypt.Net.BCrypt.GenerateSalt(12));
            await CreateAsync(user);
            return user;
        }
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await GetByIdAsync(userId);
            if (user == null) return false;

            try
            {
                if (BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword, BCrypt.Net.BCrypt.GenerateSalt(12));
                    await UpdateAsync(user);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cambiando contraseña: {ex.Message}");
            }

            return false;
        }

        private bool IsValidBCryptHash(string hash)
        {
            if (string.IsNullOrEmpty(hash))
                return false;

            // BCrypt hash debe tener 60 caracteres y empezar con $2a$, $2b$, $2x$, o $2y$
            return hash.Length == 60 &&
                   (hash.StartsWith("$2a$") ||
                    hash.StartsWith("$2b$") ||
                    hash.StartsWith("$2x$") ||
                    hash.StartsWith("$2y$"));
        }
    }
}