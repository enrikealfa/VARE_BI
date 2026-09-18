namespace SSF.PortalBI.DTOs
{
    public class UsuarioSesionDto
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
    }
}