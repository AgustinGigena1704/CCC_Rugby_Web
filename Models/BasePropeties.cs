using CCC_Rugby_Web.Models.Entityes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models
{
    public class BasePropeties : IBasePropeties
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        [Column("updated_at")]
        [DefaultValue(null)]
        public DateTime UpdatedAt { get; set; }
        [Column("updated_by")]
        [DefaultValue(null)]
        public int UpdatedBy { get; set; }
        public Usuario? UpdatedByUsuario { get; set; }
        [Column("deleted_at")]
        [DefaultValue(null)]
        public DateTime? DeletedAt { get; set; } = null;
        [Column("deleted_by")]
        [DefaultValue(null)]
        public int? DeletedBy { get; set; } = null;
        public Usuario? DeletedByUsuario { get; set; } = null;
        [Required]
        [DefaultValue(false)]
        [Column("borrado_logico")]
        public bool BorradoLogico { get; set; } = false;
    }
}
