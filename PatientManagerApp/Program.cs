using Microsoft.EntityFrameworkCore;
using PatientManagerServices.Mapper;
using PatientManagerServices.Models;
using PatientMangerServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Add patient-manager-services to the container.

builder.Services.AddAutoMapper(typeof(MapperProfile));
var conf = builder.Configuration;
builder.Services.AddDbContext<PmDbContext>(o => o.UseNpgsql(conf.GetConnectionString("Default")));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPatientServices, PatientServices>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();