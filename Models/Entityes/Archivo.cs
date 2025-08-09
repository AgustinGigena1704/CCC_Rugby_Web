using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CCC_Rugby_Web.Services.Constants;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("archivo")]
    public class Archivo : GenericEntity
    {
        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public required string Nombre { get; set; }
        [Required]
        [Column("type")]
        public required ArchivoType Type { get; set; }
        [Required]
        [Column("extension")]
        [StringLength(10)]
        public required string Extension { get; set; }  
        [Required]
        [Column("base64", TypeName = "blob")]
        public required byte[] bytes { get; set; }

        [NotMapped]
        public string Base64
        {
            get => Convert.ToBase64String(bytes);

            set => bytes = Convert.FromBase64String(value);
        }

    }
}
