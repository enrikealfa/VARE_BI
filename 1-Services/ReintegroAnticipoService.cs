using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public class ReintegroAnticipoService : IReintegroAnticipoService
    {
        private readonly SsfBcpeBiContext _context;

        public ReintegroAnticipoService(SsfBcpeBiContext context)
        {
            _context = context;
        }

        public async Task<List<ReintegroAnticipoDto>> ObtenerTodosAsync()
        {
            return await _context.ReintegroAnticipos
                .Select(r => new ReintegroAnticipoDto
                {
                    IdReintegro = r.IdReintegro,
                    NumeroUnicoPrevisional = r.NumeroUnicoPrevisional,
                    NumeroExpediente = r.NumeroExpediente,
                    FechaReintegro = r.FechaReintegro,
                    MontoReintegro = r.MontoReintegro
                })
                .ToListAsync();
        }
    }
}