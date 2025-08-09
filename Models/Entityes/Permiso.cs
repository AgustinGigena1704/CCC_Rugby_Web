using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("permiso")]
    public class Permiso : GenericEntity
    {
        [Column("nombre")]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion")]
        public string? Descripcion { get; set; }
        [Column("codigo")]
        [Required]
        public required string Codigo { get; set; }
    }
}
