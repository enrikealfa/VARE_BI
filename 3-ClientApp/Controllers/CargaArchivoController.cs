using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSF.PortalBI.Services;

namespace SSF.PortalBI.Web.Controllers
{
    [Authorize]
    public class CargaArchivoController : Controller
    {
        private readonly ICargaArchivoService _cargaArchivoService;

        public CargaArchivoController(ICargaArchivoService cargaArchivoService)
        {
            _cargaArchivoService = cargaArchivoService;
        }

        public async Task<IActionResult> Index()
        {
            var historial = await _cargaArchivoService.ObtenerHistorialAsync();
            return View(historial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(3_221_225_472)]
        public async Task<IActionResult> Upload(string tipoEntidad, IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0 || string.IsNullOrWhiteSpace(tipoEntidad))
            {
                TempData["Error"] = "Selecciona el tipo de entidad y un archivo.";
                return RedirectToAction(nameof(Index));
            }

            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            using var stream = archivo.OpenReadStream();
            var idCarga = await _cargaArchivoService.IniciarCargaAsync(tipoEntidad, stream, archivo.FileName, idUsuario);

            return RedirectToAction(nameof(Estado), new { id = idCarga });
        }

        public async Task<IActionResult> Estado(int id)
        {
            var estado = await _cargaArchivoService.ObtenerEstadoAsync(id);
            if (estado == null) return NotFound();
            return View(estado);
        }

        [HttpGet]
        public async Task<IActionResult> EstadoJson(int id)
        {
            var estado = await _cargaArchivoService.ObtenerEstadoAsync(id);
            if (estado == null) return NotFound();
            return Json(estado);
        }
    }
}