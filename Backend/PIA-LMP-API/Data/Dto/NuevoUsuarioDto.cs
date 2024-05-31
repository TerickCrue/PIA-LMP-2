namespace PIA_LMP_API.Data.Dto
{
    public class NuevoUsuarioDto
    {
        public string Nombre { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
    }
}
