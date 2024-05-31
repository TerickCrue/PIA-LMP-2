using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PIA_LMP_API.Data;

using PIA_LMP_API.Data;
using PIA_LMP_API.Data.Helpers;
using PIA_LMP_API.Services;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<PIALMPContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddCors();

//Servicios
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<MascotaPerdidaService>();
builder.Services.AddScoped<LoginService>();


// Habilita la autenticacion y establece el esquema de autenticacion predeterminado como JWT Bearer.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = BitConverter.ToString(Encoding.UTF8
                .GetBytes(builder.Configuration["JwtSettings:Issuer"])),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration["JwtSettings:Secret"])),
        };
    });

// Se configura globalmente el Token de seguridad en todos los Controllers.
// Esto obliga a las rutas a recibir un Token.
builder.Services.AddMvc(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            var exceptionType = error.GetType();

            var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is CustomException)
            {
                bool unathorized = exceptionHandlerPathFeature?.Error is UnauthorizedException;

                context.Response.StatusCode = unathorized ? 401 : 409;
                context.Response.AddApplicationError(((CustomException)error.Error).ErrorMessage);
                await context.Response.WriteAsync(((CustomException)error.Error).ErrorMessage);
            }
            else
            {
                string errorMessageDefault = "Ocurrio un error inesperado, favor de contactar al administrador del sistema";
                context.Response.AddApplicationError(errorMessageDefault);
                await context.Response.WriteAsync(errorMessageDefault);
            }
        }
    });
});

app.UseCors(builder =>
{
    builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
