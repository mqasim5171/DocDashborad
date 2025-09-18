using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Register Razor Pages services
builder.Services.AddRazorPages();  
builder.Services.AddServerSideBlazor(); 

// Register UserSessionService as Singleton to store session data
builder.Services.AddSingleton<UserSessionService>();  

// HttpClient for your API (configurable via environment variable in Railway)
var apiBase = Environment.GetEnvironmentVariable("API_BASE_URL") 
              ?? "http://localhost:5058/";  // fallback for local dev

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

// Use Razor Pages and Blazor Server-side rendering
app.MapRazorPages();  
app.MapBlazorHub();   
app.MapFallbackToPage("/_Host");  

// ✅ Railway Port Binding
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
