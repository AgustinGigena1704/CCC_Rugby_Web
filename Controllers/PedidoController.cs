using CCC_Rugby_Web.DTOs;
using CCC_Rugby_Web.Models.Repositories;
using CCC_Rugby_Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CCC_Rugby_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly EntityManager entityManager;
        public PedidoController(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        [HttpPost("GetListadoPedidos")]
        public async Task<IActionResult> GetListadoPedidos(
            [FromBody] RequestDTO requestDTO,
            CancellationToken cancellationToken = default
            )
        {
            if (requestDTO == null)
            {
                return BadRequest("Request DTO cannot be null.");
            }
            if (requestDTO.paginacion == null)
            {
                return BadRequest("Paginacion cannot be null.");
            }
            var pedidos = await entityManager.GetRepository<PedidoRepository>().GetListadoAsync(requestDTO.inicio, requestDTO.fin, requestDTO.paginacion, cancellationToken);
            return Ok(pedidos);
        }

    }
}
