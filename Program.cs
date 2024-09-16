using Clase5_proyecto.DTOs;
using Clase5_proyecto.Models;
using Clase5_proyecto.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Clase5_proyecto.Services;
using Clase5_proyecto.Automappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Esto es necesario para permitir vistas

// CONTEXTO Y ENTITY FRAMEWORK:
builder.Services.AddDbContext<PlayContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

// VALIDADORES AERONAVE:
builder.Services.AddScoped<IAeronaveService, AeronaveService>();
builder.Services.AddScoped<IValidator<AeronaveInsertDto>, AeronaveInsertValidator>();
// VALIDADORES PILOTO:
builder.Services.AddScoped<IPilotoService, PilotoService>();
builder.Services.AddScoped<IValidator<PilotoInsertDto>, PilotoInsertValidator>();
// VALIDADORES MISIÓN EMERGENCIA:
builder.Services.AddScoped<IMisionEmergenciaService, MisionEmergenciaService>();
builder.Services.AddScoped<IValidator<MisionEmergenciaInsertDto>, MisionEmergenciaInsertValidator>();

// MAPPERS:
builder.Services.AddAutoMapper(typeof(MappingProfile));


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


// DEFAULT:
app.MapControllerRoute
(
    name: "default",    
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// AERONAVES:
app.MapControllerRoute
(
    name: "default",
    pattern: "{controller=Aeronave}/{action=ListaAeronaves}/{id?}"
    // POR DEFECTO: pattern: "{controller=Home}/{action=Index}/{id?}");
);

// PILOTOS:
app.MapControllerRoute
(
    name: "default",    
    pattern: "{controller=Piloto}/{action=ListaPilotos}/{id?}"
);

// MISIÓN EMERGENCIA:
app.MapControllerRoute
(
    name: "default",
    pattern: "{controller=MisionEmergencia}/{action=ListaEmergencia}/{id?}"
);


app.Run();
