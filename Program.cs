using Microsoft.EntityFrameworkCore;
using SistemPeminjamanAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. SETTING DATABASE (Pustakawan)
// Membaca koneksi dari appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// 2. SETTING CONTROLLER (PENTING!)
// Ini biar aplikasi tau kalau ada folder Controllers
builder.Services.AddControllers();

// 3. SETTING SWAGGER (Peta API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- JALUR TIKUS (AUTO CREATE DATABASE) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}
// ------------------------------------------

// Konfigurasi Pipa Aplikasi
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); 

app.UseAuthorization();

app.MapControllers(); 

app.Run();