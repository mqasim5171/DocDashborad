var builder = WebApplication.CreateBuilder(args);

// Register Razor Pages services
builder.Services.AddRazorPages();  // Add Razor Pages services
builder.Services.AddServerSideBlazor(); // Add Blazor Server services

// Register UserSessionService as Singleton to store session data
builder.Services.AddSingleton<UserSessionService>();  // <-- Registering UserSessionService

// HttpClient for your API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5058/") // API base address
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
app.MapRazorPages();  // Ensure Razor Pages are mapped
app.MapBlazorHub();   // Blazor SignalR hub for server-side Blazor
app.MapFallbackToPage("/_Host");  // Fallback to the default Blazor host page

app.Run();