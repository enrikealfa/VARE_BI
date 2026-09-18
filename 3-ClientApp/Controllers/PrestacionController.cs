using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSF.PortalBI.Services;

namespace SSF.PortalBI.Web.Controllers
{
    [Authorize]
    public class PrestacionController : Controller
    {
        private readonly IPrestacionService _prestacionService;

        public PrestacionController(IPrestacionService prestacionService)
        {
            _prestacionService = prestacionService;
        }

        public async Task<IActionResult> Index()
        {
            var prestaciones = await _prestacionService.ObtenerTodosAsync();
            return View(prestaciones);
        }
    }
}