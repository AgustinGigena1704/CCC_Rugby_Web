using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("menu_item")]
    public class MenuItem : GenericEntity
    {
        [Column("Nombre")]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
        [Column("codigo")]
        [Required]
        public required string Codigo { get; set; }
        [Column("menu_grupo_id")]
        [Required]
        public required int MenuGrupoId { get; set; }

        public required MenuGroup MenuGrupo { get; set; }
    }
}
