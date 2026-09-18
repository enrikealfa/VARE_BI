using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSF.PortalBI.Services;

namespace SSF.PortalBI.Web.Controllers
{
    [Authorize]
    public class EmpleadorController : Controller
    {
        private readonly IEmpleadorService _empleadorService;

        public EmpleadorController(IEmpleadorService empleadorService)
        {
            _empleadorService = empleadorService;
        }

        public async Task<IActionResult> Index()
        {
            var empleadores = await _empleadorService.ObtenerTodosAsync();
            return View(empleadores);
        }
    }
}