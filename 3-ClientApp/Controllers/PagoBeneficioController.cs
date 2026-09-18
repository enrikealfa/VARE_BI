using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSF.PortalBI.Services;

namespace SSF.PortalBI.Web.Controllers
{
    [Authorize]
    public class PagoBeneficioController : Controller
    {
        private readonly IPagoBeneficioService _pagoBeneficioService;

        public PagoBeneficioController(IPagoBeneficioService pagoBeneficioService)
        {
            _pagoBeneficioService = pagoBeneficioService;
        }

        public async Task<IActionResult> Index()
        {
            var pagos = await _pagoBeneficioService.ObtenerTodosAsync();
            return View(pagos);
        }
    }
}