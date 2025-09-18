using doc.Models;
using doc.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Bind DoctorDatabaseSettings
builder.Services.Configure<DoctorDatabaseSettings>(
    builder.Configuration.GetSection("DoctorDatabaseSettings"));

// Register MongoDB client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection("DoctorDatabaseSettings").Get<DoctorDatabaseSettings>();
    return new MongoClient(settings.ConnectionString);
});

// Register DoctorService
builder.Services.AddSingleton<DoctorService>();

// Add controllers, Swagger, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("https://localhost:5230", "http://localhost:5230") // Blazor frontend dev URL
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// ✅ Use CORS
app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
