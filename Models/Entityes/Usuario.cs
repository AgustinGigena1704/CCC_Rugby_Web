using CCC_Rugby_Web.Models.Repositories;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("usuario")]
    public class Usuario : BasePropeties
    {
        [Required]
        [Column("username")]
        public required string Username { get; set; }

        [Required]
        [Column("password")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [Column("email")]
        [EmailAddress]
        public required string Email { get; set; }

        [Column("persona_id")]
        [DefaultValue(null)]
        public int? PersonaId { get; set; }
        
        public Persona? Persona { get; set; }
        [Column("avatar_archivo_id")]
        [DefaultValue(null)]
        public int? AvatarArchivoId { get; set; }
        public Archivo? AvatarArchivo { get; set; }

        [Required]
        [Column("last_login")]
        public DateTime? LastLogin { get; set; }

        [Required]
        [DefaultValue(0)]
        [Column("login_trys")]
        public int LoginTrys { get; set; } = 0;

        [Required]
        [Column("bloqueado")]
        [DefaultValue(false)]
        public bool Bloqueado { get; set; } = false;
    }
}
