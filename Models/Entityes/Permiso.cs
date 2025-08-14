using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("permiso")]
    public class Permiso : GenericEntity
    {
        [Column("nombre", Order = 1)]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion", Order = 2)]
        public string? Descripcion { get; set; }
        [Column("codigo", Order = 3)]
        [Required]
        public required string Codigo { get; set; }
    }
}
