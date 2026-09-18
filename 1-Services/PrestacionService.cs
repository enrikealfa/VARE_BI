using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public class PrestacionService : IPrestacionService
    {
        private readonly SsfBcpeBiContext _context;

        public PrestacionService(SsfBcpeBiContext context)
        {
            _context = context;
        }

        public async Task<List<PrestacionDto>> ObtenerTodosAsync()
        {
            return await _context.Prestacions
                .Select(p => new PrestacionDto
                {
                    IdPrestacion = p.IdPrestacion,
                    NumeroDocumento = p.NumeroDocumento,
                    CodigoBeneficiario = p.CodigoBeneficiario,
                    PrimerNombre = p.PrimerNombre,
                    PrimerApellido = p.PrimerApellido,
                    NumeroExpediente = p.NumeroExpediente,
                    FechaOtorgamiento = p.FechaOtorgamiento,
                    MontoPensionMensual = p.MontoPensionMensual
                })
                .ToListAsync();
        }
    }
}