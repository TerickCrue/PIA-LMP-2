using PIA_LMP_API.Data;
using PIA_LMP_API.Data.Models;
using PIA_LMP_API.Data.Dto;
using PIA_LMP_API.Data.Helpers;


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

        public UsuarioDto ConsultarUsuarioSesion(int idUsuario)
        {
            return _context.Usuarios.Where(u => u.IdUsuario == idUsuario).Select(u => new UsuarioDto
            {
                Correo = u.Correo,
                Direccion = u.Direccion,
                Nombre = u.Nombre,
                Telefono = u.Telefono,
            }).FirstOrDefault();
        }

        public Usuario? ConsultarPorId(int idUsuario)
        {
            return _context.Usuarios.Find(idUsuario);
        }

        public IEnumerable<Usuario> ConsultarPorCorreo(usuarioCorreoDto correo)
        {
            return _context.Usuarios.Where(u => u.Correo == correo.Correo).ToList();
        }

        public void AgregarUsuario(NuevoUsuarioDto usuarioNuevo)
        {
         
            var usuarioExistente = _context.Usuarios.Where(u => u.Correo.Equals(usuarioNuevo.Correo)).FirstOrDefault();

            if (usuarioExistente is not null)
            {
                throw new CustomException("Usuario ya registrado");
            }

            var usuario = new Usuario
            {
                Nombre = usuarioNuevo.Nombre,
                Correo = usuarioNuevo.Correo,
                Contrasena = usuarioNuevo.Contrasena,
                Telefono = usuarioNuevo.Telefono,
            };

            var usuarioAgregado = _context.Usuarios.Add(usuario);
            _context.SaveChanges();

        }

        public UsuarioDto ActualizarUsuario(int idUsuario, UsuarioDto usuarioActualizar)
        {
            var existingUsuario = ConsultarPorId(idUsuario);

            if(existingUsuario is not null)
            {
                existingUsuario.Nombre = usuarioActualizar.Nombre;
                existingUsuario.Direccion = usuarioActualizar.Direccion;
                existingUsuario.Telefono = usuarioActualizar.Telefono;

                _context.SaveChanges();

                return new UsuarioDto
                {
                    Nombre = existingUsuario.Nombre,
                    Correo = existingUsuario.Correo,
                    Telefono = existingUsuario.Telefono,
                    Direccion = existingUsuario.Direccion,
                };
            }
            else
            {
                throw new CustomException("Usuario no válido");
            }
        }

        public void EliminarUsuario(Usuario usuarioEliminar)
        {
            _context.Usuarios.Remove(usuarioEliminar);
            _context.SaveChanges();
        }


    }
}
