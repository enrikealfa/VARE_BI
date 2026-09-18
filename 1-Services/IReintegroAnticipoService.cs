using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface IReintegroAnticipoService
    {
        Task<List<ReintegroAnticipoDto>> ObtenerTodosAsync();
    }
}