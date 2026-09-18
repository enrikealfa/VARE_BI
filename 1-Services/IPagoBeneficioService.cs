using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface IPagoBeneficioService
    {
        Task<List<PagoBeneficioDto>> ObtenerTodosAsync();
    }
}