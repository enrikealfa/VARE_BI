using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public class MoraEmpleadorService : IMoraEmpleadorService
    {
        private readonly SsfBcpeBiContext _context;

        public MoraEmpleadorService(SsfBcpeBiContext context)
        {
            _context = context;
        }

        public async Task<List<MoraEmpleadorDto>> ObtenerTodosAsync()
        {
            return await _context.MoraEmpleadors
                .Select(m => new MoraEmpleadorDto
                {
                    IdMora = m.IdMora,
                    Nit = m.Nit,
                    MontoOmis = m.MontoOmis,
                    MontoDnp = m.MontoDnp,
                    MontoIns = m.MontoIns,
                    TotalMora = m.TotalMora
                })
                .ToListAsync();
        }
    }
}