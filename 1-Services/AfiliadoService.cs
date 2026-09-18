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

        public async Task<PagedResultDto<AfiliadoDto>> ObtenerPaginadoAsync(int pagina, int tamanoPagina, string? busqueda)
        {
            if (pagina < 1) pagina = 1;
            if (tamanoPagina < 1 || tamanoPagina > 200) tamanoPagina = 25;

            var query = _context.Afiliados.AsQueryable();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                var texto = busqueda.Trim();
                query = query.Where(a =>
                    a.NumeroDocumento.Contains(texto) ||
                    a.PrimerNombre.Contains(texto) ||
                    a.PrimerApellido.Contains(texto) ||
                    (a.Nup != null && a.Nup.Contains(texto)));
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(a => a.PrimerApellido).ThenBy(a => a.PrimerNombre)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
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
                    IdTipoSistema = a.IdTipoSistema,
                    Nup = a.Nup,
                    Dui = a.Dui,
                    Nit = a.Nit,
                    TipoAfiliado = a.TipoAfiliado,
                    FechaAfiliacion = a.FechaAfiliacion
                })
                .ToListAsync();

            return new PagedResultDto<AfiliadoDto>
            {
                Items = items,
                PaginaActual = pagina,
                TamanoPagina = tamanoPagina,
                TotalRegistros = total
            };
        }

        public async Task<AfiliadoDto?> ObtenerPorIdAsync(int idAfiliado)
        {
            var resultado = await ObtenerPaginadoAsync(1, 1, null);
            return await _context.Afiliados
                .Where(a => a.IdAfiliado == idAfiliado)
                .Select(a => new AfiliadoDto
                {
                    IdAfiliado = a.IdAfiliado,
                    NumeroDocumento = a.NumeroDocumento,
                    PrimerNombre = a.PrimerNombre,
                    PrimerApellido = a.PrimerApellido,
                    FechaNacimiento = a.FechaNacimiento,
                    Genero = a.Genero,
                    EstadoFamiliar = a.EstadoFamiliar,
                    EstadoAfiliado = a.EstadoAfiliado,
                    Nup = a.Nup,
                    Dui = a.Dui,
                    Nit = a.Nit
                })
                .FirstOrDefaultAsync();
        }
    }
}