using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface ICargaArchivoService
    {
        Task<int> IniciarCargaAsync(string tipoEntidad, Stream archivoStream, string nombreArchivoOriginal, int idUsuarioCarga);
        Task<CargaArchivoEstadoDto?> ObtenerEstadoAsync(int idCargaArchivo);
        Task<List<CargaArchivoResumenDto>> ObtenerHistorialAsync();
    }
}