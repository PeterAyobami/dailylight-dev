using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dailylight.Web.Server.Admin
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        /// <summary>
        /// The javascript runtime interop
        /// </summary>
        private readonly IJSRuntime mJsRuntime;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="jsRuntime">The inject jsruntime</param>
        public TokenAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            mJsRuntime = jsRuntime;
        }

        /// <summary>
        /// Gets authentication token from clients memory
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTokenAsync()
            => await mJsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        /// <summary>
        /// Sets authentication token in client side memory
        /// </summary>
        /// <param name="token">The specified token</param>
        public async Task SetTokenAsync(string token)
        {
            if (token == null)
            {
                await mJsRuntime.InvokeAsync<object>("localStorage.removeItem", "authToken");
            }
            else
            {
                await mJsRuntime.InvokeAsync<object>("localStorage.setItem", "authToken", token);
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task RemoveTokenAsync() =>
            await mJsRuntime.InvokeAsync<object>("localStorage.removeItem", "authToken");

        /// <summary>
        /// Gets authentication state from claims
        /// </summary>
        /// <returns>The authentication state</returns>
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Get token
            var token = await GetTokenAsync();

            // Get identity from token
            var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity()
                : new ClaimsIdentity(ServiceExtensions.ParseClaimsFromJwt(token), "jwt");

            // Return authentication state
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
