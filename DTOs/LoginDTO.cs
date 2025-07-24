using System.ComponentModel.DataAnnotations;

namespace CCC_Rugby_Web.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Debe ingresar el nombre de usuario o correo")]
        public string Username { get; set; }   
        [Required(ErrorMessage = "Debe ingresar la contraseña")]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
