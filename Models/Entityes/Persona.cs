using CCC_Rugby_Web.Models.Entityes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("persona")]
    public class Persona : GenericEntity
    {
        [Required]
        [Column("tipo_documento", Order = 1)]
        public required string TipoDocumento { get; set; }
        [Required]
        [Column("documento", Order = 2)]
        public required string Documento { get; set; }
        [Required]
        [Column("nombres", Order = 3)]
        public required string Nombres { get; set; }
        [Required]
        [Column("apellidos", Order = 4)]
        public required string Apellidos { get; set; }
        [Required]  
        [Column("fecha_nacimiento", Order = 5)]
        public required DateOnly FechaNacimiento { get; set; }
        [Required]
        [Column("genero", Order = 6)]
        public required string Genero { get; set; }
    }
}
