using ChatAPI.Models.DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5188", "http://192.168.6.98:5188");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatAPI", Version = "v1" });
});

//builder.Services.AddDbContext<DBCHAT>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ChatAPI_Connection")));

builder.Services.Configure<RequestLocalizationOptions>(
               opts =>
               {
                   var supportedCultures = new[] { new CultureInfo("es-MX"), };
                   opts.DefaultRequestCulture = new RequestCulture("es-MX");
                   opts.SupportedCultures = supportedCultures;
                   opts.SupportedUICultures = supportedCultures;
               });

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,// validacion del emisor de la petici�n falso
        ValidateAudience = false,// validacion del servidor de entrada falso
        ValidateIssuerSigningKey = true,// validacion de la firma del token true
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:TokenJWT").Value!)),
        ValidateLifetime = true,// valida tiempo de expiracion del token true
        RequireExpirationTime = true,// requerir en el token exista una fecha de expiraci�n true 
        ClockSkew = TimeSpan.Zero // Forzar que el token expire exactamente cuando el tiempo de expieraci�n del mismo token indique
    };
});

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyHeader()
    .AllowAnyMethod()
    //AllowAnyOrigin();
    .SetIsOriginAllowed((host) => true)
    .AllowCredentials();
}));

//builder.Services.AddScoped<IGRAPI, GRAPIService>();
//builder.Services.AddScoped<INotificaciones, NotificacionesService>();
//builder.Services.AddScoped<ISeguridad, SeguridadService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatAPI v1"));
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
