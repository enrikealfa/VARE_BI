using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.BusinessClass.Seed;
using SSF.PortalBI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Conexion a la base de datos SSF_BCPE_BI (LocalDB)
builder.Services.AddDbContext<SsfBcpeBiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de servicios de negocio
builder.Services.AddScoped<IAfiliadoService, AfiliadoService>();
builder.Services.AddScoped<IEmpleadorService, EmpleadorService>();
builder.Services.AddScoped<ICotizanteService, CotizanteService>();
builder.Services.AddScoped<IMoraEmpleadorService, MoraEmpleadorService>();
builder.Services.AddScoped<IPrestacionService, PrestacionService>();
builder.Services.AddScoped<IPagoBeneficioService, PagoBeneficioService>();
builder.Services.AddScoped<IReintegroAnticipoService, ReintegroAnticipoService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Autenticacion por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Seed inicial de roles y usuario administrador
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SsfBcpeBiContext>();
    DbInitializer.SeedUsuarioAdmin(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();