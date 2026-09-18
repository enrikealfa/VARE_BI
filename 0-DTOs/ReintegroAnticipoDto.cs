namespace SSF.PortalBI.DTOs
{
    public class ReintegroAnticipoDto
    {
        public long IdReintegro { get; set; }
        public string NumeroUnicoPrevisional { get; set; } = string.Empty;
        public string NumeroExpediente { get; set; } = string.Empty;
        public DateOnly? FechaReintegro { get; set; }
        public decimal? MontoReintegro { get; set; }
    }
}