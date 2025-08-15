using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("rol")]
    public class Rol : GenericEntity
    {
        [Required]
        [StringLength(100)]
        [Column("nombre", Order = 1)]
        public required string Name { get; set; }


        [StringLength(250)]
        [Column("descripcion", Order = 2)]
        public string? Descripcion { get; set; } = null;
        [Column("codigo", Order = 3 )]
        [Required]
        public required string Codigo { get; set; }

    }
}
