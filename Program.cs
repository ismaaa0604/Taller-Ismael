using datos;
using interfaces;
using repositorios;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. REGISTRO DE SERVICIOS (Antes de Build)
// ==========================================

builder.Services.AddControllersWithViews();

// Obtención e inyección de la cadena de conexión encapsulada en un tipo propio
var cadena = builder.Configuration.GetConnectionString("SqliteConnectionString")!;
builder.Services.AddSingleton(new ConexionDb(cadena));

// Registro de Inyección de Dependencias para los Repositorios
builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>();
// Necesario si AutenticacionService va a leer/escribir en la Session
builder.Services.AddHttpContextAccessor();

// Registro directo sin interfaz
//builder.Services.AddSingleton<IUsuarioRepository,UsuarioRepository>();
//builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();

// Configuración de Sesión (en memoria + cookie)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ==========================================
// 2. PIPELINE HTTP (Después de Build)
// ==========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();

// El middleware de sesión debe ir después de UseRouting y antes de UseAuthorization/MapControllerRoute

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
