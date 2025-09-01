using CCC_Rugby_Web.DTOs;
using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;
using Microsoft.EntityFrameworkCore;

namespace CCC_Rugby_Web.Models.Repositories
{
    [Repository(typeof(Menu))]
    public class MenuRepository : GenericRepository<Menu>
    {
        public MenuRepository(CCC_DbContext context, EntityManager entityManager, IUserContextService userContextService) : base(context, entityManager, userContextService)
        {
        }

        public async Task<MenuDTO> GetMenuDtoByCodigo(string codigo, Usuario ActualUser)
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
            var roles = ActualUser.Roles;


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
    }
}
