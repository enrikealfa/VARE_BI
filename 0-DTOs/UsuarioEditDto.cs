using System.ComponentModel.DataAnnotations;

namespace SSF.PortalBI.DTOs
{
    public class UsuarioEditDto
    {
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo invalido.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        public bool Activo { get; set; }

        [Display(Name = "Nueva clave (dejar vacio para no cambiarla)")]
        [MinLength(8, ErrorMessage = "La clave debe tener al menos 8 caracteres.")]
        public string? NuevaClave { get; set; }

        [Display(Name = "Roles")]
        public List<int> RolesSeleccionados { get; set; } = new();

        public List<RolOptionDto> RolesDisponibles { get; set; } = new();
    }
}