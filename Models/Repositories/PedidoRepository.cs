using CCC_Rugby_Web.DTOs;
using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;
using Microsoft.EntityFrameworkCore;

namespace CCC_Rugby_Web.Models.Repositories
{
    [Repository(typeof(Pedido))]
    public class PedidoRepository : GenericRepository<Pedido>
    {
        public PedidoRepository(CCC_DbContext context, EntityManager entityManager) : base(context, entityManager)
        {
        }

        public async Task<ListadoDTO<Pedido>> GetListadoAsync(DateTime inicio, DateTime fin, PaginacionDTO paginacion, CancellationToken cancellationToken = default, TipoArticulo? tipo = null, bool total = false)
        {
            ListadoDTO<Pedido> Listado = new ListadoDTO<Pedido>();
            var query = context.Pedidos.Where(p =>
                    p.BorradoLogico != true
                    && DateTime.Compare(p.CreatedAt, inicio) >= 0
                    && DateTime.Compare(p.CreatedAt, fin) <= 0)
                    .Include(p => p.Articulos)
                    .AsQueryable();


            if (total)
            {
                Listado.TotalItems = await query.CountAsync(cancellationToken);
            }

            if (tipo != null)
            {
                query = query.Where(p => p.Articulos.Any(a => a.TipoArticuloId == tipo.Id));
            }

            var pedidos = await query
                .Skip((paginacion.Page - 1) * paginacion.RecordsPerPage)
                .Take(paginacion.RecordsPerPage)
                .ToListAsync(cancellationToken);
            return Listado;
        }
    }
}
