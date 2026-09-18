using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public class CotizanteService : ICotizanteService
    {
        private readonly SsfBcpeBiContext _context;

        public CotizanteService(SsfBcpeBiContext context)
        {
            _context = context;
        }

        public async Task<List<CotizanteDto>> ObtenerTodosAsync()
        {
            return await _context.Cotizantes
                .Select(c => new CotizanteDto
                {
                    IdCotizante = c.IdCotizante,
                    NumeroDocumento = c.NumeroDocumento,
                    NitEmpleador = c.NitEmpleador,
                    Ibc = c.Ibc,
                    PeriodoDevengue = c.PeriodoDevengue,
                    IdPlanilla = c.IdPlanilla,
                    SituacionLaboral = c.SituacionLaboral,
                    IdTipoCotizante = c.IdTipoCotizante
                })
                .ToListAsync();
        }
    }
}