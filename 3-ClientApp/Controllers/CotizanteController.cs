using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSF.PortalBI.Services;

namespace SSF.PortalBI.Web.Controllers
{
    [Authorize]
    public class CotizanteController : Controller
    {
        private readonly ICotizanteService _cotizanteService;

        public CotizanteController(ICotizanteService cotizanteService)
        {
            _cotizanteService = cotizanteService;
        }

        public async Task<IActionResult> Index()
        {
            var cotizantes = await _cotizanteService.ObtenerTodosAsync();
            return View(cotizantes);
        }
    }
}