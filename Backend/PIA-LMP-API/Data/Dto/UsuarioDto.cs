namespace PIA_LMP_API.Data.Dto
{
    public class UsuarioDto
    {
        public string Nombre { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string? Direccion { get; set; }
    }
}
