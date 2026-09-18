using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface IPrestacionService
    {
        Task<List<PrestacionDto>> ObtenerTodosAsync();
    }
}