using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.BusinessClass.Entities;
using SSF.PortalBI.BusinessClass.Security;
using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SsfBcpeBiContext _context;

        public UsuarioService(SsfBcpeBiContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioDto>> ObtenerTodosAsync()
        {
            return await _context.Usuarios
                .Include(u => u.IdRols)
                .Select(u => new UsuarioDto
                {
                    IdUsuario = u.IdUsuario,
                    NombreUsuario = u.NombreUsuario,
                    Correo = u.Correo,
                    NombreCompleto = u.NombreCompleto,
                    Activo = u.Activo,
                    Bloqueado = u.Bloqueado,
                    Roles = u.IdRols.Select(r => r.NombreRol).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<RolOptionDto>> ObtenerRolesDisponiblesAsync()
        {
            return await _context.Rols
                .Where(r => r.Activo)
                .Select(r => new RolOptionDto { IdRol = r.IdRol, NombreRol = r.NombreRol })
                .ToListAsync();
        }

        public async Task<UsuarioEditDto?> ObtenerParaEditarAsync(int idUsuario)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.IdRols)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            if (usuario == null) return null;

            var roles = await ObtenerRolesDisponiblesAsync();
            var idsAsignados = usuario.IdRols.Select(r => r.IdRol).ToHashSet();
            foreach (var rol in roles)
                rol.Seleccionado = idsAsignados.Contains(rol.IdRol);

            return new UsuarioEditDto
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                Correo = usuario.Correo,
                NombreCompleto = usuario.NombreCompleto,
                Activo = usuario.Activo,
                RolesSeleccionados = idsAsignados.ToList(),
                RolesDisponibles = roles
            };
        }

        public async Task<(bool exito, string mensaje)> CrearAsync(UsuarioCreateDto dto)
        {
            var existeUsuario = await _context.Usuarios.AnyAsync(u => u.NombreUsuario == dto.NombreUsuario);
            if (existeUsuario)
                return (false, "Ya existe un usuario con ese nombre.");

            var existeCorreo = await _context.Usuarios.AnyAsync(u => u.Correo == dto.Correo);
            if (existeCorreo)
                return (false, "Ya existe un usuario con ese correo.");

            var (hash, salt) = PasswordHasher.HashPassword(dto.Clave);

            var usuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                Correo = dto.Correo,
                NombreCompleto = dto.NombreCompleto,
                ClaveHash = hash,
                ClaveSalt = salt,
                Activo = true
            };

            if (dto.RolesSeleccionados.Any())
            {
                var roles = await _context.Rols.Where(r => dto.RolesSeleccionados.Contains(r.IdRol)).ToListAsync();
                foreach (var rol in roles)
                    usuario.IdRols.Add(rol);
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return (true, "Usuario creado correctamente.");
        }

        public async Task<(bool exito, string mensaje)> ActualizarAsync(UsuarioEditDto dto)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.IdRols)
                .FirstOrDefaultAsync(u => u.IdUsuario == dto.IdUsuario);

            if (usuario == null)
                return (false, "Usuario no encontrado.");

            var correoDuplicado = await _context.Usuarios
                .AnyAsync(u => u.Correo == dto.Correo && u.IdUsuario != dto.IdUsuario);
            if (correoDuplicado)
                return (false, "Ya existe otro usuario con ese correo.");

            usuario.Correo = dto.Correo;
            usuario.NombreCompleto = dto.NombreCompleto;
            usuario.Activo = dto.Activo;

            if (!string.IsNullOrWhiteSpace(dto.NuevaClave))
            {
                var (hash, salt) = PasswordHasher.HashPassword(dto.NuevaClave);
                usuario.ClaveHash = hash;
                usuario.ClaveSalt = salt;
            }

            usuario.IdRols.Clear();
            if (dto.RolesSeleccionados.Any())
            {
                var roles = await _context.Rols.Where(r => dto.RolesSeleccionados.Contains(r.IdRol)).ToListAsync();
                foreach (var rol in roles)
                    usuario.IdRols.Add(rol);
            }

            await _context.SaveChangesAsync();
            return (true, "Usuario actualizado correctamente.");
        }

        public async Task<bool> CambiarEstadoActivoAsync(int idUsuario, bool activo)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null) return false;

            usuario.Activo = activo;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}