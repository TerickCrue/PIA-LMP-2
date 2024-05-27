using PIA_LMP_API.Data;
using PIA_LMP_API.Data.Models;
using PIA_LMP_API.Data.Dto;


namespace PIA_LMP_API.Services
{
    public class UsuarioService
    {
        private readonly PIALMPContext _context;

        public UsuarioService(PIALMPContext context) {
            _context = context;
        }

        public IEnumerable<Usuario> ConsultarTodosUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario? ConsultarPorId(int idUsuario)
        {
            return _context.Usuarios.Find(idUsuario);
        }

        public IEnumerable<Usuario> ConsultarPorCorreo(usuarioCorreoDto correo)
        {
            return _context.Usuarios.Where(u => u.Correo == correo.Correo).ToList();
        }

        public Usuario AgregarUsuario(Usuario usuarioNuevo)
        {
         
            var usuarioExistente = _context.Usuarios.Where(u => u.Correo.Equals(usuarioNuevo.Correo)).FirstOrDefault();
            if (usuarioExistente is not null)
            {
                throw new Exception("Usuario ya registrado");
            }

            _context.Usuarios.Add(usuarioNuevo);
            _context.SaveChanges();

            return usuarioNuevo;

        }

        public void ActualizarUsuario(Usuario usuarioActualizar)
        {
            var existingUsuario = ConsultarPorId(usuarioActualizar.IdUsuario);

            if(existingUsuario is not null)
            {
                existingUsuario.Nombre = usuarioActualizar.Nombre;
                existingUsuario.Direccion = usuarioActualizar.Direccion;

                _context.SaveChanges();
            }
        }

        public void EliminarUsuario(Usuario usuarioEliminar)
        {
            _context.Usuarios.Remove(usuarioEliminar);
            _context.SaveChanges();
        }


    }
}
