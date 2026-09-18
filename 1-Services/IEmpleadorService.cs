using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface IEmpleadorService
    {
        Task<List<EmpleadorDto>> ObtenerTodosAsync();
    }
}