using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("persona")]
    public class Persona : GenericEntity
    {
        [Required]
        [Column("tipo_documento")]
        public required string TipoDocumento { get; set; }
        [Required]
        [Column("documento")]
        public required string Documento { get; set; }
        [Required]
        [Column("nombres")]
        public required string Nombres { get; set; }
        [Required]
        [Column("apellidos")]
        public required string Apellidos { get; set; }
        [Required]  
        [Column("fecha_nacimiento")]
        public required DateOnly FechaNacimiento { get; set; }
        [Required]
        [Column("genero")]
        public required string Genero { get; set; }
    }
}
