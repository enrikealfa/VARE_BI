using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public class PagoBeneficioService : IPagoBeneficioService
    {
        private readonly SsfBcpeBiContext _context;

        public PagoBeneficioService(SsfBcpeBiContext context)
        {
            _context = context;
        }

        public async Task<List<PagoBeneficioDto>> ObtenerTodosAsync()
        {
            return await _context.PagoBeneficios
                .Select(p => new PagoBeneficioDto
                {
                    IdPagoBeneficio = p.IdPagoBeneficio,
                    NumeroDocumento = p.NumeroDocumento,
                    CodigoBeneficiario = p.CodigoBeneficiario,
                    FechaPago = p.FechaPago,
                    MontoPagado = p.MontoPagado,
                    IdFuenteFondos = p.IdFuenteFondos
                })
                .ToListAsync();
        }
    }
}