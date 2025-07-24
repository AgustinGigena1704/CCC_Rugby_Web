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
        public async Task SetCookieAsync(string name, string value, int days)
        {
            await jsRuntime.InvokeVoidAsync("setCookie", name, value, days);
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
