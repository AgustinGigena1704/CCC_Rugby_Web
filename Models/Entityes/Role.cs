using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("rol")]
    public class Role : GenericEntity
    {
        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public required string Name { get; set; }


        [StringLength(250)]
        [Column("descripcion")]
        public string? Descripcion { get; set; } = null;
        [Column("codigo")]
        [Required]
        public required string Codigo { get; set; }

    }
}
