using BlazorWebAssemblyStandalone;
using BlazorWebAssemblyStandalone.Constants;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(AppConstants.HTTP_CLIENT_NAME, client => client.BaseAddress = new Uri(AppConstants.BASE_URL));

await builder.Build().RunAsync();
