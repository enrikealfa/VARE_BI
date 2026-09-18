using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSF.PortalBI.Services;

namespace SSF.PortalBI.Web.Controllers
{
    [Authorize]
    public class ReintegroAnticipoController : Controller
    {
        private readonly IReintegroAnticipoService _reintegroAnticipoService;

        public ReintegroAnticipoController(IReintegroAnticipoService reintegroAnticipoService)
        {
            _reintegroAnticipoService = reintegroAnticipoService;
        }

        public async Task<IActionResult> Index()
        {
            var reintegros = await _reintegroAnticipoService.ObtenerTodosAsync();
            return View(reintegros);
        }
    }
}