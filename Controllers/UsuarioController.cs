using CCC_Rugby_Web.DTOs;
using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Models.Repositories;
using CCC_Rugby_Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static CCC_Rugby_Web.Services.Constants;

namespace CCC_Rugby_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : GenericController
    {
        public UsuarioController(EntityManager entityManager) : base(entityManager) { }

        [HttpGet("GetAvatar/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAvatar(int userId)
        {
            var usuario = await entityManager.GetRepository<UsuarioRepository>().GetByIdAsync(userId);
            if (usuario == null)
            {
                return NotFound();
            }
            var avatar = usuario.AvatarArchivo;
            if (avatar == null)
            {
                return NotFound("El usuario no tiene avatar");
            }
            return Ok(avatar);
        }

        [HttpPost("UpdateAvatar/{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar(int userId, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo de avatar.");
            }

            var usuarioRepo = entityManager.GetRepository<UsuarioRepository>();
            var usuario = await usuarioRepo.GetByIdAsync(userId);
            if (usuario == null)
            {
                return NotFound();
            }
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            var archivoRepository = entityManager.GetRepository<ArchivoRepository>();
            if (usuario.AvatarArchivo != null)
            {
                // Eliminar el archivo anterior si existe
                await archivoRepository.DeleteAsync(usuario.AvatarArchivo);
            }

            Archivo newAvatar = new Archivo
            {
                Nombre = file.FileName,
                Type = ArchivoType.Image,
                bytes = fileBytes,
                CreatedBy = usuario.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = usuario.Id,
                UpdatedAt = DateTime.UtcNow,
                Extension = Path.GetExtension(file.FileName).TrimStart('.').ToLowerInvariant(),
                CreatedByUsuario = usuario,
                UpdatedByUsuario = usuario
            };
            await archivoRepository.CreateAsync(newAvatar);
            usuario.AvatarArchivo = newAvatar;
            usuario.AvatarArchivoId = newAvatar.Id;
            await usuarioRepo.UpdateAsync(usuario);
            await entityManager.SaveChangesAsync();
            return Ok("Avatar actualizado correctamente.");
        }

        
    }
}
