using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    public class GenericEntity : BasePropeties
    {
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        [Column("created_by")]
        public int CreatedBy { get; set; }
        public required Usuario CreatedByUsuario { get; set; }
        
    }
}
