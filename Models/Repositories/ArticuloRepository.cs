using CCC_Rugby_Web.DTOs;
using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;
using Microsoft.EntityFrameworkCore;

namespace CCC_Rugby_Web.Models.Repositories
{
    [Repository(typeof(Articulo))]
    public class ArticuloRepository : GenericRepository<Articulo>
    {
        public ArticuloRepository(CCC_DbContext context, EntityManager entityManager, IUserContextService userContextService) : base(context, entityManager, userContextService)
        {
        }

        public async Task<List<TipoArticuloDTO>> GetTipoArticulos()
        {
            return await context.TipoArticulos
                .Where(t => !t.BorradoLogico)
                .OrderBy(t => t.Nombre)
                .Select(ta => new TipoArticuloDTO
                {
                   Id = ta.Id,
                   Nombre = ta.Nombre
                })
                .ToListAsync();
        }
    }
}
