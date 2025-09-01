using CCC_Rugby_Web.Models.Repositories;
using CCC_Rugby_Web.Services;
using CCC_Rugby_Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CCC_Rugby_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : GenericController
    {
        public ArticuloController(EntityManager entityManager) : base(entityManager) { }

        [HttpGet("GetTipoArticulos")]
        public async Task<IActionResult> GetTipoArticulos()
        {
             var tipoArticulos = await entityManager.GetRepository<ArticuloRepository>().GetTipoArticulos();
            var tipoArticuloDTOs = tipoArticulos.Select(t => new TipoArticuloDTO
            {
                Id = t.Id,
                Nombre = t.Nombre
            }).ToList();
            return Ok(tipoArticuloDTOs);
        }
    }
}
