using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSF.PortalBI.Services;

namespace SSF.PortalBI.Web.Controllers
{
    [Authorize]
    public class AfiliadoController : Controller
    {
        private readonly IAfiliadoService _afiliadoService;

        public AfiliadoController(IAfiliadoService afiliadoService)
        {
            _afiliadoService = afiliadoService;
        }

        public async Task<IActionResult> Index(int pagina = 1, string? busqueda = null)
        {
            var resultado = await _afiliadoService.ObtenerPaginadoAsync(pagina, 25, busqueda);
            ViewData["Busqueda"] = busqueda;
            return View(resultado);
        }
    }
}