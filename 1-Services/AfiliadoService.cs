using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public class AfiliadoService : IAfiliadoService
    {
        private readonly SsfBcpeBiContext _context;

        public AfiliadoService(SsfBcpeBiContext context)
        {
            _context = context;
        }

        public async Task<List<AfiliadoDto>> ObtenerTodosAsync()
        {
            return await _context.Afiliados
                .Select(a => new AfiliadoDto
                {
                    IdAfiliado = a.IdAfiliado,
                    NumeroDocumento = a.NumeroDocumento,
                    IdTipoDocumento = a.IdTipoDocumento,
                    PrimerNombre = a.PrimerNombre,
                    SegundoNombre = a.SegundoNombre,
                    PrimerApellido = a.PrimerApellido,
                    SegundoApellido = a.SegundoApellido,
                    FechaNacimiento = a.FechaNacimiento,
                    Genero = a.Genero,
                    EstadoFamiliar = a.EstadoFamiliar,
                    EstadoAfiliado = a.EstadoAfiliado,
                    CodigoPais = a.CodigoPais,
                    IdTipoSistema = a.IdTipoSistema
                })
                .ToListAsync();
        }

        public async Task<AfiliadoDto?> ObtenerPorIdAsync(int idAfiliado)
        {
            var lista = await ObtenerTodosAsync();
            return lista.FirstOrDefault(a => a.IdAfiliado == idAfiliado);
        }
    }
}