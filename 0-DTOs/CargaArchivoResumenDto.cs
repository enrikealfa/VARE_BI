namespace SSF.PortalBI.DTOs
{
    public class CargaArchivoResumenDto
    {
        public int IdCargaArchivo { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public string TipoEntidad { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCarga { get; set; }
        public int? TotalRegistros { get; set; }
        public int? RegistrosCorrectos { get; set; }
        public int? RegistrosError { get; set; }
    }
}