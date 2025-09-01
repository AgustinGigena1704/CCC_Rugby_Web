using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;
using Microsoft.EntityFrameworkCore;

namespace CCC_Rugby_Web.Models.Repositories
{
    [Repository(typeof(Archivo))]
    public class ArchivoRepository : GenericRepository<Archivo>
    {
        public ArchivoRepository(CCC_DbContext context, EntityManager entityManager, IUserContextService userContextService) : base(context, entityManager, userContextService)
        {
        }

        public async Task<Archivo?> GetUserAvatar(Usuario user)
        {
            return await context.Usuarios
                .Where(u => u.Id == user.Id && u.AvatarArchivo != null)
                .Include(u => u.AvatarArchivo)
                .Select(u => u.AvatarArchivo)
                .FirstOrDefaultAsync();
        }
    }
}
