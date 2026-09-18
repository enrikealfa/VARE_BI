namespace SSF.PortalBI.DTOs
{
    public class AfiliadoDto
    {
        public int IdAfiliado { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public int IdTipoDocumento { get; set; }
        public string PrimerNombre { get; set; } = string.Empty;
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; } = string.Empty;
        public string? SegundoApellido { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string EstadoFamiliar { get; set; } = string.Empty;
        public string EstadoAfiliado { get; set; } = string.Empty;
        public string CodigoPais { get; set; } = string.Empty;
        public int IdTipoSistema { get; set; }
    }
}