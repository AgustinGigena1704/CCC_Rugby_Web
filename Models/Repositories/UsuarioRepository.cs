using CCC_Rugby_Web.Models.Entityes;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CCC_Rugby_Web.Models.Repositories
{
    public class UsuarioRepository
    {
        private readonly CCC_DbContext context;
        public UsuarioRepository(CCC_DbContext _context)
        {
            context = _context;
        }
        
        public async Task<Usuario?> GetById(int id)
        {
            return await context.Usuarios
                .Include(u => u.Persona)
                .FirstOrDefaultAsync(u => u.Id == id && !u.BorradoLogico);
        }

        public async Task<Usuario?> GetByPassAndUser(string username, string password)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Usuario? user;
            
            if (Regex.IsMatch(username, patron))
            {
                user = await context.Usuarios
                    .Include(u => u.Persona)
                    .FirstOrDefaultAsync(u => u.Email == username && u.Password == password && !u.BorradoLogico);   
            }
            else
            {
                user = await context.Usuarios
                    .Include(u => u.Persona)
                    .FirstOrDefaultAsync(u => u.Username == username && u.Password == password && !u.BorradoLogico);
            }
            
            if (user != null)
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
    }
}
