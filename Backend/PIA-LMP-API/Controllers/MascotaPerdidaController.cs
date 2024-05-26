using Microsoft.AspNetCore.Mvc;
using PIA_LMP_API.Data.Models;
using PIA_LMP_API.Services;

namespace PIA_LMP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotaPerdidaController: ControllerBase
    {

        private MascotaPerdidaService _mascotaService;

        public MascotaPerdidaController(MascotaPerdidaService mascotaService)
        {
            this._mascotaService = mascotaService;
        }

        [HttpGet("consultarMascotas")]
        public IEnumerable<MascotaPerdidum> ConsultarUsuarios()
        {
            return _mascotaService.ConsultarTodasMascotas();
        }

        [HttpGet("consultarPorId/{idMascota}")]
        public MascotaPerdidum ConsultarPorId(int idMascota)
        {
            return _mascotaService.ConsultarPorId(idMascota);
        }

        [HttpPost("agregar")]
        public MascotaPerdidum Agregar(MascotaPerdidum mascota)
        {
            return _mascotaService.AgregarMascota(mascota);
        }

        [HttpPut("actualizar")]
        public void Actualizar(MascotaPerdidum mascota)
        {
            _mascotaService.ActualizarMascota(mascota);
        }

        [HttpDelete("eliminar")]
        public void Eliminar(MascotaPerdidum mascota)
        {
            _mascotaService.EliminarMascota(mascota);
        }

    }
}
