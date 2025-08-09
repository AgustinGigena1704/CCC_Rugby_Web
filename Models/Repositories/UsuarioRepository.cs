using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static BCrypt.Net.BCrypt;

namespace CCC_Rugby_Web.Models.Repositories
{
    [Repository(typeof(Usuario))]
    public class UsuarioRepository : GenericRepository<Usuario>
    {
        public UsuarioRepository(CCC_DbContext context) : base(context)
        {
        }

        public async Task<Usuario?> GetById(int id)
        {
            return await context.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.AvatarArchivo)
                .FirstOrDefaultAsync(u => u.Id == id && !u.BorradoLogico);
        }

        public void Update(Usuario user)
        {
            if (!IsValidBCryptHash(user.Password))
            {
                user.Password = HashPassword(user.Password);
            }
            context.Usuarios.Update(user);

        }

        public async Task<Usuario?> GetByPassAndUser(string username, string password)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Usuario? user;

            if (Regex.IsMatch(username, patron))
            {
                user = await context.Usuarios
                    .Include(u => u.Persona)
                    .Include(u => u.AvatarArchivo)
                    .FirstOrDefaultAsync(u => u.Email == username && !u.BorradoLogico);
            }
            else
            {
                user = await context.Usuarios
                    .FirstOrDefaultAsync(u => u.Username == username && !u.BorradoLogico);
            }

            if (user != null && Verify(password, HashPassword(user.Password)))
            {
                user.LastLogin = DateTime.UtcNow;
                await context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<List<Role>> GetRoles(int userId)
        {
            var roles = await context.UsuarioRoles
                .Where(ur => ur.UsuarioId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role)
                .ToListAsync();
            return roles;
        }

        private bool IsValidBCryptHash(string hash)
        {
            if (string.IsNullOrEmpty(hash))
                return false;

            return hash.Length == 60 &&
                   (hash.StartsWith("$2a$") ||
                    hash.StartsWith("$2b$") ||
                    hash.StartsWith("$2x$") ||
                    hash.StartsWith("$2y$"));
        }
    }
}
