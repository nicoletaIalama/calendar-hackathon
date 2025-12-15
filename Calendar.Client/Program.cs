using Calendar.Client;
using Calendar.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
    var baseAddress = string.IsNullOrWhiteSpace(apiBaseUrl)
        ? new Uri(builder.HostEnvironment.BaseAddress)
        : new Uri(apiBaseUrl, UriKind.Absolute);

    return new HttpClient { BaseAddress = baseAddress };
});

builder.Services.AddScoped<EmployeeApiClient>();
builder.Services.AddScoped<BrowserLocalStorage>();
builder.Services.AddScoped<EmployeeViewsStore>();
builder.Services.AddScoped<EmployeeHolidayApiClient>();

await builder.Build().RunAsync();


