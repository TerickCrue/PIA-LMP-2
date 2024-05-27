using System;
using System.Collections.Generic;

namespace PIA_LMP_API.Data.Models
{
    public partial class MascotaPerdidum
    {
        public int IdMascota { get; set; }
        public string Nombre { get; set; } = null!;
        public string Latitud { get; set; } = null!;
        public string Longitud { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Recompensa { get; set; }
        public DateTime Fecha { get; set; }
    }
}
