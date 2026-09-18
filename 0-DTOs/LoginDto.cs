using System.ComponentModel.DataAnnotations;

namespace SSF.PortalBI.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [Display(Name = "Clave")]
        public string Clave { get; set; } = string.Empty;
    }
}