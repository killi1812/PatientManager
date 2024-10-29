using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PatientManagerServices;
using PatientManagerServices.Models;
using PatientManagerServices.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MapperProfile));
var conf = builder.Configuration;
builder.Services.AddDbContext<PmDbContext>(o => o.UseNpgsql(conf.GetConnectionString("Default")));

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

