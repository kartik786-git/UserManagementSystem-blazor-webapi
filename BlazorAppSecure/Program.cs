using BlazorAppSecure;
using BlazorAppSecure.Sevices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CutomHttpHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = 
    new Uri(builder.Configuration["FrontendUrl"] ??  "https://localhost:5002") });


builder.Services.AddHttpClient("Auth", opt => opt.BaseAddress =
new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7030"))
    .AddHttpMessageHandler<CutomHttpHandler>();

await builder.Build().RunAsync();
