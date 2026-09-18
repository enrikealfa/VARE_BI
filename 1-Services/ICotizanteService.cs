using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface ICotizanteService
    {
        Task<List<CotizanteDto>> ObtenerTodosAsync();
    }
}