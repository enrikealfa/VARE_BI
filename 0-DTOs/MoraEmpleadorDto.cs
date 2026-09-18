namespace SSF.PortalBI.DTOs
{
    public class MoraEmpleadorDto
    {
        public long IdMora { get; set; }
        public string Nit { get; set; } = string.Empty;
        public decimal MontoOmis { get; set; }
        public decimal MontoDnp { get; set; }
        public decimal MontoIns { get; set; }
        public decimal TotalMora { get; set; }
    }
}