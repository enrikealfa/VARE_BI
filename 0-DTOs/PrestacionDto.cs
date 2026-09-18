namespace SSF.PortalBI.DTOs
{
    public class PrestacionDto
    {
        public long IdPrestacion { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string CodigoBeneficiario { get; set; } = string.Empty;
        public string PrimerNombre { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string NumeroExpediente { get; set; } = string.Empty;
        public DateOnly FechaOtorgamiento { get; set; }
        public decimal? MontoPensionMensual { get; set; }
    }
}