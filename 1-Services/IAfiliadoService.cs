using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface IAfiliadoService
    {
        Task<List<AfiliadoDto>> ObtenerTodosAsync();
        Task<AfiliadoDto?> ObtenerPorIdAsync(int idAfiliado);
    }
}