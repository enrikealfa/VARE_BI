namespace SSF.PortalBI.DTOs
{
    public class EmpleadorDto
    {
        public int IdEmpleador { get; set; }
        public string Nit { get; set; } = string.Empty;
        public int IdTipoPersona { get; set; }
        public string? RazonSocial { get; set; }
        public string? PrimerNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public int IdTipoEmpleador { get; set; }
        public string CodigoCentroTrabajo { get; set; } = string.Empty;
        public string? NombreCentroTrabajo { get; set; }
    }
}