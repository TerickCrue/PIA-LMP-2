using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIA_LMP_API.Data.Dto;
using PIA_LMP_API.Data.Helpers;
using PIA_LMP_API.Data.Models;
using PIA_LMP_API.Services;
//using PIA_LMP_API.Helpers;


namespace PIA_LMP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService) {

            this._usuarioService = usuarioService;

        }

        [HttpGet("consultarTodosUsuarios")]
        public IEnumerable<Usuario> ConsultarUsuarios()
        {
            return _usuarioService.ConsultarTodosUsuarios();
        }

        [HttpGet("consultarUsuario")]
        public UsuarioDto ConsultarUsuarioSesion()
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
            return _usuarioService.ConsultarUsuarioSesion(idUsuario);
        }

        [HttpGet("consultarPorId/{idUsuario}")]
        public Usuario ConsultarPorId(int idUsuario){
            return _usuarioService.ConsultarPorId(idUsuario);
        }

        [HttpGet("consultarPorCorreo")]
        public IEnumerable<Usuario> ConsultarPorCorreo(usuarioCorreoDto correo)
        {
            return _usuarioService.ConsultarPorCorreo(correo);
        }

        [AllowAnonymous]
        [HttpPost("agregar")]
        public void Agregar(NuevoUsuarioDto usuario)
        {
            _usuarioService.AgregarUsuario(usuario);
        }

        [HttpPut("actualizar")]
        public UsuarioDto Actualizar(UsuarioDto usuario)
        {
            int idUsuario = Utileria.ObtenerIdUsuarioSesion(this);
           return _usuarioService.ActualizarUsuario(idUsuario, usuario);
        }

        [HttpDelete("eliminar")]
        public void Eliminar(Usuario usuario)
        {
            _usuarioService.EliminarUsuario(usuario);
        }




    }
}
