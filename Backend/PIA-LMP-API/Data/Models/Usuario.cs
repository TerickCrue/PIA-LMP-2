using System;
using System.Collections.Generic;

namespace PIA_LMP_API.Data.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string? Direccion { get; set; }
    }
}
