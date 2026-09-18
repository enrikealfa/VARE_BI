using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.DTOs;

namespace SSF.PortalBI.Services
{
    public class EmpleadorService : IEmpleadorService
    {
        private readonly SsfBcpeBiContext _context;

        public EmpleadorService(SsfBcpeBiContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadorDto>> ObtenerTodosAsync()
        {
            return await _context.Empleadors
                .Select(e => new EmpleadorDto
                {
                    IdEmpleador = e.IdEmpleador,
                    Nit = e.Nit,
                    IdTipoPersona = e.IdTipoPersona,
                    RazonSocial = e.RazonSocial,
                    PrimerNombre = e.PrimerNombre,
                    PrimerApellido = e.PrimerApellido,
                    IdTipoEmpleador = e.IdTipoEmpleador,
                    CodigoCentroTrabajo = e.CodigoCentroTrabajo,
                    NombreCentroTrabajo = e.NombreCentroTrabajo
                })
                .ToListAsync();
        }
    }
}