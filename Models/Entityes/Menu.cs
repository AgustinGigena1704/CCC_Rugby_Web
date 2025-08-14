using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("menu")]
    public class Menu : GenericEntity
    {
        [Column("nombre", Order = 1)]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion", Order = 2)]
        [DefaultValue(null)]
        public string? Descripcion { get; set; } = null;
        [Column("codigo", Order = 3)]
        [Required]
        public required string Codigo { get; set; }
    }
}
