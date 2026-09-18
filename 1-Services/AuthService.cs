using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.BusinessClass.Security;
using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public class AuthService : IAuthService
    {
        private readonly SsfBcpeBiContext _context;

        public AuthService(SsfBcpeBiContext context)
        {
            _context = context;
        }

        public async Task<UsuarioSesionDto?> ValidarCredencialesAsync(string nombreUsuario, string clave)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.IdRols)
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario && u.Activo);

            if (usuario == null || usuario.Bloqueado)
                return null;

            if (!PasswordHasher.VerifyPassword(clave, usuario.ClaveHash, usuario.ClaveSalt))
                return null;

            usuario.FechaUltimoAcceso = DateTime.Now;
            await _context.SaveChangesAsync();

            return new UsuarioSesionDto
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                NombreCompleto = usuario.NombreCompleto,
                Roles = usuario.IdRols.Select(r => r.NombreRol).ToList()
            };
        }
    }
}