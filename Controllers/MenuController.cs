using CCC_Rugby_Web.DTOs;
using CCC_Rugby_Web.Models.Repositories;
using CCC_Rugby_Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace CCC_Rugby_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : GenericController
    {
        public MenuController(EntityManager entityManager) : base(entityManager)
        {
        }

        [HttpGet("GetMainMenu/{menuCodigo}")]
        public async Task<IActionResult> GetMainMenu(string menuCodigo)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Usuario no autenticado");
            var usuario = await entityManager.GetRepository<UsuarioRepository>().GetByIdAsync(int.Parse(userId));
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }
            MenuDTO menu = await entityManager.GetRepository<MenuRepository>().GetMenuDtoByCodigo(menuCodigo, usuario);
            if (menu.MenuGrupos.Count <= 0)
            {
                return NotFound("No se encontraron los item del menu");
            }
            return Ok(menu);

        }
    }
}
