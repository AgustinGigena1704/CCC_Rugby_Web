using Microsoft.JSInterop;

namespace CCC_Rugby_Web.Services
{
    public class CookieService
    {
        private readonly IJSRuntime jsRuntime;

        public CookieService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }
        public async Task SetCookieAsync(string name, string value, DateTime vencimiento)
        {
            string x = vencimiento.ToString("R");
            string expires = x.Replace("GMT", "UTC");
            await jsRuntime.InvokeVoidAsync("setCookie", name, value, expires);
        }

        public async Task DeleteCookieAsync(string name)
        {
            await jsRuntime.InvokeVoidAsync("deleteCookie", name);
        }

        public async Task<string> GetCookieAsync(string name)
        {
            return await jsRuntime.InvokeAsync<string>("getCookie", name);
        }
    }
}
