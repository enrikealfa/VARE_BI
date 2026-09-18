using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSF.PortalBI.Services;

namespace SSF.PortalBI.Web.Controllers
{
    [Authorize]
    public class MoraEmpleadorController : Controller
    {
        private readonly IMoraEmpleadorService _moraEmpleadorService;

        public MoraEmpleadorController(IMoraEmpleadorService moraEmpleadorService)
        {
            _moraEmpleadorService = moraEmpleadorService;
        }

        public async Task<IActionResult> Index()
        {
            var registros = await _moraEmpleadorService.ObtenerTodosAsync();
            return View(registros);
        }
    }
}