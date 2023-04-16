using Microsoft.EntityFrameworkCore;
using Demo.DAL.DataContext;
using Demo.Models;
using Demo.DAL.Repository;
using Demo.BLL.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TrabajadoresPruebaContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("cadenaConexion"));
});

builder.Services.AddScoped<ITrabajadoresRepository,TrabajadoresRepository>();
builder.Services.AddScoped<ITrabajadoresService,TrabajadoresService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
