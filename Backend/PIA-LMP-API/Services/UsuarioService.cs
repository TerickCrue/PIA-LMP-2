using PIA_LMP_API.Data;
using PIA_LMP_API.Data.Models;

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

        public IEnumerable<Usuario> ConsultarPorCorreo(string correoUsuario)
        {
            return _context.Usuarios.Where(u => u.Correo == correoUsuario).ToList();
        }

        public Usuario AgregarUsuario(Usuario usuarioNuevo)
        {
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
