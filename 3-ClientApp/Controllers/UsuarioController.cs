using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSF.PortalBI.DTOs;
using SSF.PortalBI.Services;

namespace SSF.PortalBI.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.ObtenerTodosAsync();
            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dto = new UsuarioCreateDto
            {
                RolesDisponibles = await _usuarioService.ObtenerRolesDisponiblesAsync()
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                dto.RolesDisponibles = await _usuarioService.ObtenerRolesDisponiblesAsync();
                return View(dto);
            }

            var (exito, mensaje) = await _usuarioService.CrearAsync(dto);
            if (!exito)
            {
                ModelState.AddModelError(string.Empty, mensaje);
                dto.RolesDisponibles = await _usuarioService.ObtenerRolesDisponiblesAsync();
                return View(dto);
            }

            TempData["Mensaje"] = mensaje;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _usuarioService.ObtenerParaEditarAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                dto.RolesDisponibles = await _usuarioService.ObtenerRolesDisponiblesAsync();
                return View(dto);
            }

            var (exito, mensaje) = await _usuarioService.ActualizarAsync(dto);
            if (!exito)
            {
                ModelState.AddModelError(string.Empty, mensaje);
                dto.RolesDisponibles = await _usuarioService.ObtenerRolesDisponiblesAsync();
                return View(dto);
            }

            TempData["Mensaje"] = mensaje;
            return RedirectToAction(nameof(Index));
        }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> CambiarEstado(int idUsuario, bool activo)
{
    await _usuarioService.CambiarEstadoActivoAsync(idUsuario, activo);
    return RedirectToAction(nameof(Index));
}
    }
}