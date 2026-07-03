using BlazorWebApp.Client.Constants;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient(AppConstants.HTTP_CLIENT_NAME, client => client.BaseAddress = new Uri(AppConstants.BASE_URL));

await builder.Build().RunAsync();
