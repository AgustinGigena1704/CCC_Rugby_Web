using CCC_Rugby_Web.DTOs;
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

        public async Task<MenuDTO> GetMenuDtoByCodigo(string codigo, int userId)
        {
            var menu = await context.Menus
                .FirstOrDefaultAsync(m => m.Codigo == codigo && !m.BorradoLogico);
            var r = new MenuDTO();
            if (menu == null)
                return r;
            var roles = await GetRoles(userId);
            if (roles == null || !roles.Any())
                return r;
            if (roles.Select(r => r.Codigo).Contains("admin"))
            {
                var AmenuGroups = await context.MenuGroups
                    .Where(mg => !mg.BorradoLogico)
                    .ToListAsync();
                
                foreach (var grupo in AmenuGroups)
                {
                    var AmenuItems = await context.MenuItems
                    .Where(mi => !mi.BorradoLogico && grupo.Id == mi.MenuGrupoId)
                    .ToListAsync();
                    List<MenuItemDTO> itemsDto = new List<MenuItemDTO>();
                    foreach (var item in AmenuItems)
                    {
                        itemsDto.Add(new MenuItemDTO
                        {
                            Nombre = item.Nombre,
                            Icono = item.Icono,
                            Url = item.Url
                        });
                    }
                    r.MenuGrupos.Add(new MenuGroupDTO
                    {
                        Nombre = grupo.Nombre,
                        Icono = grupo.Icono,
                        MenuItems = itemsDto
                    });
                }
                return r;
            }

            var menuGroups = await context.MenuGroups
                .Where(mg => !mg.BorradoLogico && mg.MenuId == menu.Id && roles.Select(r => r.Id).Contains(mg.RolId??0))
                .ToListAsync();
            

            foreach (var grupo in menuGroups)
            {
                var menuItems = await context.MenuItems
                .Where(mi => !mi.BorradoLogico && grupo.Id == mi.MenuGrupoId)
                .ToListAsync();
                List<MenuItemDTO> itemsDto = new List<MenuItemDTO>();
                foreach (var item in menuItems)
                {
                    itemsDto.Add(new MenuItemDTO
                    {
                        Nombre = item.Nombre,
                        Icono = item.Icono,
                        Url = item.Url
                    });
                }
                r.MenuGrupos.Add(new MenuGroupDTO
                {
                    Nombre = grupo.Nombre,
                    Icono = grupo.Icono,
                    MenuItems = itemsDto
                });
            }
                return r;
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
