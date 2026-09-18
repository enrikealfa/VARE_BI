using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.BusinessClass.Entities;
using SSF.PortalBI.DTOs;
using SSF.PortalBI.Services.Infraestructura;
using Microsoft.Extensions.DependencyInjection;

namespace SSF.PortalBI.Services
{
    public class CargaArchivoService : ICargaArchivoService
    {
        private static readonly HashSet<string> TiposValidos = new()
        {
            "afiliado", "empleador", "cotizante", "mora_empleador",
            "prestacion", "pago_beneficio", "reintegro_anticipo"
        };

        private readonly SsfBcpeBiContext _context;
        private readonly IBackgroundTaskQueue _queue;
        private readonly IWebHostEnvironment _env;

        public CargaArchivoService(SsfBcpeBiContext context, IBackgroundTaskQueue queue, IWebHostEnvironment env)
        {
            _context = context;
            _queue = queue;
            _env = env;
        }

        public async Task<int> IniciarCargaAsync(string tipoEntidad, Stream archivoStream, string nombreArchivoOriginal, int idUsuarioCarga)
        {
            if (!TiposValidos.Contains(tipoEntidad))
                throw new ArgumentException("Tipo de entidad no valido.");

            var carga = new CargaArchivo
            {
                NombreArchivo = nombreArchivoOriginal,
                TipoEntidad = tipoEntidad,
                IdUsuarioCarga = idUsuarioCarga,
                Estado = "PENDIENTE"
            };
            _context.CargaArchivos.Add(carga);
            await _context.SaveChangesAsync();

            var carpetaCargas = Path.Combine(_env.ContentRootPath, "App_Data", "CargasPendientes");
            Directory.CreateDirectory(carpetaCargas);
            var extension = Path.GetExtension(nombreArchivoOriginal);
            var rutaDestino = Path.Combine(carpetaCargas, $"{carga.IdCargaArchivo}{extension}");

            using (var destino = new FileStream(rutaDestino, FileMode.Create))
            {
                await archivoStream.CopyToAsync(destino);
            }

            carga.RutaArchivo = rutaDestino;
            await _context.SaveChangesAsync();

            var idCarga = carga.IdCargaArchivo;
            await _queue.EncolarAsync(async (sp, ct) =>
            {
                var procesador = sp.GetRequiredService<ICargaArchivoProcesadorService>();
                await procesador.ProcesarAsync(idCarga, ct);
            });

            return idCarga;
        }

        public async Task<CargaArchivoEstadoDto?> ObtenerEstadoAsync(int idCargaArchivo)
        {
            var carga = await _context.CargaArchivos.FindAsync(idCargaArchivo);
            if (carga == null) return null;

            return new CargaArchivoEstadoDto
            {
                IdCargaArchivo = carga.IdCargaArchivo,
                NombreArchivo = carga.NombreArchivo,
                TipoEntidad = carga.TipoEntidad,
                Estado = carga.Estado,
                TotalRegistros = carga.TotalRegistros,
                RegistrosCorrectos = carga.RegistrosCorrectos,
                RegistrosError = carga.RegistrosError,
                FechaCarga = carga.FechaCarga,
                FechaFinProceso = carga.FechaFinProceso
            };
        }

        public async Task<List<CargaArchivoResumenDto>> ObtenerHistorialAsync()
        {
            return await _context.CargaArchivos
                .OrderByDescending(c => c.FechaCarga)
                .Take(50)
                .Select(c => new CargaArchivoResumenDto
                {
                    IdCargaArchivo = c.IdCargaArchivo,
                    NombreArchivo = c.NombreArchivo,
                    TipoEntidad = c.TipoEntidad,
                    Estado = c.Estado,
                    FechaCarga = c.FechaCarga,
                    TotalRegistros = c.TotalRegistros,
                    RegistrosCorrectos = c.RegistrosCorrectos,
                    RegistrosError = c.RegistrosError
                })
                .ToListAsync();
        }
    }
}