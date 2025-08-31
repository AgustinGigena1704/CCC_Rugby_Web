using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
// ¡Importante! Añadir el using para acceder a tu constante
using static CCC_Rugby_Web.Services.Constants;

namespace CCC_Rugby_Web.Security
{
    /// <summary>
    /// Este DelegatingHandler intercepta las solicitudes salientes de HttpClient
    /// y reenvía la cookie de autenticación desde el HttpContext actual.
    /// Es crucial para que las llamadas de API desde Blazor Server a sus propios controladores
    /// se realicen como el usuario autenticado.
    /// </summary>
    public class CookieForwardingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieForwardingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                // CORRECCIÓN: Usamos el nombre de la cookie correcto desde tus constantes.
                var cookieName = TOKEN_COOKIE_NAME;
                var cookieValue = httpContext.Request.Cookies[cookieName];

                if (!string.IsNullOrEmpty(cookieValue))
                {
                    // Eliminamos cualquier cabecera de Cookie existente para evitar duplicados
                    request.Headers.Remove("Cookie");

                    // Añadimos la cookie a la petición saliente del HttpClient
                    request.Headers.Add("Cookie", $"{cookieName}={cookieValue}");
                }
            }

            // Continuamos con la petición
            return await base.SendAsync(request, cancellationToken);
        }
    }
}