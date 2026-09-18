using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface IAfiliadoService
    {
        Task<PagedResultDto<AfiliadoDto>> ObtenerPaginadoAsync(int pagina, int tamanoPagina, string? busqueda);
        Task<AfiliadoDto?> ObtenerPorIdAsync(int idAfiliado);
    }
}