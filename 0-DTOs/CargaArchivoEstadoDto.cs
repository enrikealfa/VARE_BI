namespace SSF.PortalBI.DTOs
{
    public class CargaArchivoEstadoDto
    {
        public int IdCargaArchivo { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public string TipoEntidad { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public int? TotalRegistros { get; set; }
        public int? RegistrosCorrectos { get; set; }
        public int? RegistrosError { get; set; }
        public DateTime FechaCarga { get; set; }
        public DateTime? FechaFinProceso { get; set; }
    }
}