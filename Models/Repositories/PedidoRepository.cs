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

        public async Task<ListadoDTO<PedidoDTO>> GetListadoAsync(DateTime inicio, DateTime fin, PaginacionDTO paginacion, CancellationToken cancellationToken = default, TipoArticulo? tipo = null)
        {
            ListadoDTO<PedidoDTO> Listado = new ListadoDTO<PedidoDTO>();
            try
            {

                var query = context.Pedidos.Where(p =>
                        p.BorradoLogico != true
                        && inicio.Date <= p.Fecha
                        && fin.Date >= p.Fecha)
                        .AsQueryable();
                if (tipo != null)
                {
                    query = query.Where(p => p.Articulos.Any(a => a.TipoArticuloId == tipo.Id));
                }

                if (paginacion.GetTotalItems)
                {
                    Listado.TotalItems = await query.CountAsync(cancellationToken);
                }

                var pedidos = await query
                    .Include(p => p.Articulos)
                    .Skip((paginacion.Page) * paginacion.RecordsPerPage)
                    .Take(paginacion.RecordsPerPage)
                    .Select(p => new PedidoDTO
                    {
                        Id = p.Id,
                        Fecha = p.Fecha,
                        NombreComprador = p.NombreComprador,
                        CantidadArticulos = p.Articulos.Count,
                        DireccionEntrega = p.DireccionEntrega,
                        EstadoId = p.EstadoId,
                        EstadoNombre = p.Estado.Nombre,
                        TipoPagoId = p.TipoPagoId,
                        TipoPagoNombre = p.TipoPago != null ? p.TipoPago.Nombre : null,
                        UsuarioUsername = p.Usuario.Username
                    })
                    .ToListAsync(cancellationToken);

                Listado.Listado = pedidos;
            }
            catch (Exception ex)
            {
                // Manejo de errores (log, rethrow, etc.)
                throw new Exception("Error retrieving pedidos", ex);
            }
            return Listado;
        }
    }
}
