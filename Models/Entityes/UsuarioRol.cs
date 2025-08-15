using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("usuario_rol")]
    public class UsuarioRol
    {
        [Key]
        [Required]
        public required int UsuarioId { get; set; }
        [Required]
        public required int RoleId { get; set; }
        
        public required Usuario Usuario { get; set; }
        public required Rol Role { get; set; }
    }
}
