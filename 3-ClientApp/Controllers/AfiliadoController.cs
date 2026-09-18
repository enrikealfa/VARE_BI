using Microsoft.AspNetCore.Mvc;
using SSF.PortalBI.Services;

namespace SSF.PortalBI.Web.Controllers
{
    public class AfiliadoController : Controller
    {
        private readonly IAfiliadoService _afiliadoService;

        public AfiliadoController(IAfiliadoService afiliadoService)
        {
            _afiliadoService = afiliadoService;
        }

        public async Task<IActionResult> Index()
        {
            var afiliados = await _afiliadoService.ObtenerTodosAsync();
            return View(afiliados);
        }
    }
}