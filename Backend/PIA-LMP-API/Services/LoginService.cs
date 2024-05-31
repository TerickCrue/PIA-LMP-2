using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PIA_LMP_API.Data;
using PIA_LMP_API.Data.Dto;
using PIA_LMP_API.Data.Helpers;
using PIA_LMP_API.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PIA_LMP_API.Services
{
    public class LoginService
    {
        private readonly PIALMPContext _context;
        private IConfiguration _config;

        public LoginService(PIALMPContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private Usuario GetUsuario(LoginRequest request)
        {
            return _context.Usuarios.
                SingleOrDefault(u => u.Correo == request.Correo && u.Contrasena == request.Contrasena);
        }

        public LoginResponse Authenticate(LoginRequest request)
        {

            var usuario = GetUsuario(request);

            if(usuario is null)
            {
                throw new CustomException("usuario y/o contraseña incorrectos");
            }
            else
            {
                string token = GenerateToken(usuario);

                return new LoginResponse
                {
                    Token = token,
                    Nombre = usuario.Nombre,
                    Usuario = usuario.Correo
                };
            }

        }

        private string GenerateToken(Usuario usuario)
        {
            var issuer = Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:Issuer").Value);
            var key = Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:Secret").Value);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);;

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nombre),
        };

            var securityToken = new JwtSecurityToken(
                issuer: BitConverter.ToString(issuer),
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

    }
}
