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
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt(12));
            }
            context.Usuarios.Update(user);
        }

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
                        .FirstOrDefaultAsync(u => u.Email == username && !u.BorradoLogico);
                }
                else
                {
                    user = await context.Usuarios
                        .Include(u => u.Persona)
                        .Include(u => u.AvatarArchivo)
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

        public async Task<List<Rol>> GetRoles(int userId)
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
            var r = new MenuDTO()
            {
                Nombre = menu?.Nombre ?? "Menu Principal",
                Descripcion = menu?.Descripcion
            };

            if (menu == null)
                return r;

            var roles = await GetRoles(userId);
            if (roles == null || !roles.Any())
                return r;

            if (roles.Select(r => r.Codigo).Contains("admin"))
            {
                var AmenuGroups = await context.MenuGroups
                    .Where(mg => !mg.BorradoLogico && mg.MenuId == menu.Id)
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
                .Where(mg => !mg.BorradoLogico && mg.MenuId == menu.Id && roles.Select(r => r.Id).Contains(mg.RolId ?? 0))
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

        // Método para crear usuario con contraseña hasheada
        public async Task<Usuario> CreateUserAsync(Usuario user, string plainPassword)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(plainPassword, BCrypt.Net.BCrypt.GenerateSalt(12));
            await context.Usuarios.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        // Método para cambiar contraseña
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await GetById(userId);
            if (user == null) return false;

            try
            {
                if (BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword, BCrypt.Net.BCrypt.GenerateSalt(12));
                    context.Usuarios.Update(user);
                    await context.SaveChangesAsync();
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