
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace BlazorAppSecure.Sevices
{
    public class CutomHttpHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            request.Headers.Add("X-Requested-With", ["XMLHttpRequest"]);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
