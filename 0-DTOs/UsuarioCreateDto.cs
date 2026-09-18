using System.ComponentModel.DataAnnotations;

namespace SSF.PortalBI.DTOs
{
    public class UsuarioCreateDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo invalido.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [MinLength(8, ErrorMessage = "La clave debe tener al menos 8 caracteres.")]
        public string Clave { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirma la clave.")]
        [Compare("Clave", ErrorMessage = "Las claves no coinciden.")]
        [Display(Name = "Confirmar clave")]
        public string ConfirmarClave { get; set; } = string.Empty;

        [Display(Name = "Roles")]
        public List<int> RolesSeleccionados { get; set; } = new();

        public List<RolOptionDto> RolesDisponibles { get; set; } = new();
    }
}