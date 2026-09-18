using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface IAuthService
    {
        Task<UsuarioSesionDto?> ValidarCredencialesAsync(string nombreUsuario, string clave);
    }
}