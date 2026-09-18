using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> ObtenerTodosAsync();
        Task<UsuarioEditDto?> ObtenerParaEditarAsync(int idUsuario);
        Task<List<RolOptionDto>> ObtenerRolesDisponiblesAsync();
        Task<(bool exito, string mensaje)> CrearAsync(UsuarioCreateDto dto);
        Task<(bool exito, string mensaje)> ActualizarAsync(UsuarioEditDto dto);
        Task<bool> CambiarEstadoActivoAsync(int idUsuario, bool activo);
    }
}