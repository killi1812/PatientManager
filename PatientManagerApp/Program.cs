using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PatientManagerServices;
using PatientManagerServices.Models;
using PatientManagerServices.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MapperProfile));
var conf = builder.Configuration;
builder.Services.AddDbContext<PmDbContext>(o => o.UseNpgsql(conf.GetConnectionString("Default")));

string keyb = builder.Configuration["key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        byte[] key = Encoding.UTF8.GetBytes(keyb);
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IIllnessService, IllnessService>();
builder.Services.AddScoped<IExaminationService, ExaminationService>();
builder.Services.AddScoped<IPrescriptionService,PrescriptionService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IMedicalHistoryService, MedicalHistoryService>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
Directory.CreateDirectory("./tmp");
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
app.UseAuthentication();
app.UseAuthorization();
app.Run();

