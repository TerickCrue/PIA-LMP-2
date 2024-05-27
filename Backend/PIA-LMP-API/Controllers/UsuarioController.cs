using Microsoft.AspNetCore.Mvc;
using PIA_LMP_API.Data.Dto;
using PIA_LMP_API.Data.Models;
using PIA_LMP_API.Services;

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

        [HttpGet("consultarUsuarios")]
        public IEnumerable<Usuario> ConsultarUsuarios()
        {
            return _usuarioService.ConsultarTodosUsuarios();
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

        [HttpPost("agregar")]
        public Usuario Agregar(Usuario usuario)
        {
            return _usuarioService.AgregarUsuario(usuario);
        }

        [HttpPut("actualizar")]
        public void Actualizar(Usuario usuario)
        {
           _usuarioService.ActualizarUsuario(usuario);
        }

        [HttpDelete("eliminar")]
        public void Eliminar(Usuario usuario)
        {
            _usuarioService.EliminarUsuario(usuario);
        }




    }
}
