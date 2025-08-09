using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("rol_permiso")]
    public class RolPermiso
    {
        [Column("rol_id")]
        [Required]
        public int RolId { get; set; }
        [Column("permiso_id")]
        [Required]
        public int PermisoId { get; set; }
        public required Role Rol { get; set; }
        public required Permiso Permiso { get; set; }
    }
}
