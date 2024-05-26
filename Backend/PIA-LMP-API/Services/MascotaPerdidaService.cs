using PIA_LMP_API.Data;
using PIA_LMP_API.Data.Models;

namespace PIA_LMP_API.Services
{
    public class MascotaPerdidaService
    {
        private readonly PIALMPContext _context;

        public MascotaPerdidaService(PIALMPContext context)
        {
            _context = context;
        }

        public IEnumerable<MascotaPerdidum> ConsultarTodasMascotas()
        {
            return _context.MascotaPerdida.ToList();
        }

        public MascotaPerdidum? ConsultarPorId(int idMascota)
        {
            return _context.MascotaPerdida.Find(idMascota);
        }

        public MascotaPerdidum AgregarMascota(MascotaPerdidum mascotaNuevo)
        {
            _context.MascotaPerdida.Add(mascotaNuevo);
            _context.SaveChanges();

            return mascotaNuevo;

        }

        public void ActualizarMascota(MascotaPerdidum mascotaActualizar)
        {
            var existingMascota = ConsultarPorId(mascotaActualizar.IdMascota);

            if (existingMascota is not null)
            {
                existingMascota.Nombre = mascotaActualizar.Nombre;
                existingMascota.Descripcion = mascotaActualizar.Descripcion;
                existingMascota.Recompensa = mascotaActualizar.Recompensa;
                existingMascota.Fecha = mascotaActualizar.Fecha;

                _context.SaveChanges();
            }
        }

        public void EliminarMascota(MascotaPerdidum usuarioEliminar)
        {
            _context.MascotaPerdida.Remove(usuarioEliminar);
            _context.SaveChanges();
        }

    }
}
