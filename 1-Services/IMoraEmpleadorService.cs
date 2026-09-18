using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface IMoraEmpleadorService
    {
        Task<List<MoraEmpleadorDto>> ObtenerTodosAsync();
    }
}