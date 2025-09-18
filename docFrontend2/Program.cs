using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Register Razor Pages services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register UserSessionService as Singleton to store session data
builder.Services.AddSingleton<UserSessionService>();

// ✅ HttpClient for your API (env variable with fallback)
var apiBase = Environment.GetEnvironmentVariable("API_BASE_URL");
if (string.IsNullOrEmpty(apiBase))
{
    apiBase = builder.Environment.IsDevelopment()
        ? "http://localhost:5058/" // local dev API
        : "https://reasonable-exploration-production.up.railway.app/"; // production fallback
}

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBase)
});

var app = builder.Build();

// Middleware configuration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// ✅ Railway Port Binding
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
