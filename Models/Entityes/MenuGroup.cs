using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("menu_grupo")]
    public class MenuGroup : GenericEntity
    {
        [Column("nombre")]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion")]
        [Required]
        public required string Descripcion { get; set; }
        [Column("rol_id")]
        [Required]
        public int RolId { get; set; }
        [Column("iconPath")]
        [Required]
        [DefaultValue(Icons.Material.Filled.Group)]
        public required string Icon { get; set; }

        public required Role Rol { get; set; }
    }
}
