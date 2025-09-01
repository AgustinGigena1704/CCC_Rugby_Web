using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Models.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CCC_Rugby_Web.Services
{
    public interface IUserContextService
    {
        Task<Usuario?> GetActualUserAsync();
    }

    public class UserContextService : IUserContextService
    {
        private readonly AuthService authService;
        private readonly EntityManager em;

        public UserContextService(AuthService authService, EntityManager em)
        {
            this.authService = authService;
            this.em = em;
        }

        public async Task<Usuario?> GetActualUserAsync()
        {
            var user = await authService.GetAuthUserAsync();
            if (user != null)
            {
                var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId != null && int.TryParse(userId, out int id))
                {
                    return await em.GetRepository<UsuarioRepository>().GetByIdAsync(id);
                }
            }
            return null;
        }
    }
}
