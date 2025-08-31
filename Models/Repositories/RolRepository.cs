using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;
using Microsoft.EntityFrameworkCore;

namespace CCC_Rugby_Web.Models.Repositories
{
    [Repository(typeof(Rol))]
    public class RolRepository : GenericRepository<Rol>
    {
        public RolRepository(CCC_DbContext context, EntityManager entityManager, IUserContextService userContextService) : base(context, entityManager, userContextService)
        {
        }

        public async Task<List<Rol>> GetRolesByUsuario(Usuario user)
        {
            List<Rol> roles = new List<Rol>();

            var userWithRoles = await context.Usuarios
                .Where(u => u.Id == user.Id && !u.BorradoLogico)
                .Include(u => u.Roles.Where(r => !r.BorradoLogico))
                .FirstOrDefaultAsync();
            roles = userWithRoles?.Roles.ToList() ?? new List<Rol>();

            return roles;
        }
    }
}
