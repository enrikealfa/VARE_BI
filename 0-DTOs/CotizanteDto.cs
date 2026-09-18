namespace SSF.PortalBI.DTOs
{
    public class CotizanteDto
    {
        public long IdCotizante { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string NitEmpleador { get; set; } = string.Empty;
        public decimal Ibc { get; set; }
        public int PeriodoDevengue { get; set; }
        public int IdPlanilla { get; set; }
        public string SituacionLaboral { get; set; } = string.Empty;
        public int IdTipoCotizante { get; set; }
    }
}