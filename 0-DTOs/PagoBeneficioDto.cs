namespace SSF.PortalBI.DTOs
{
    public class PagoBeneficioDto
    {
        public long IdPagoBeneficio { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string CodigoBeneficiario { get; set; } = string.Empty;
        public DateOnly FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public int IdFuenteFondos { get; set; }
    }
}