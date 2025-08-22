using CCC_Rugby_Web.Models.Repositories;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("usuario")]
    public class Usuario : GenericEntity<WithoutCreated>
    {
        [Required]
        [Column("username", Order = 1)]
        public required string Username { get; set; }

        [Required]
        [Column("password", Order = 2)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [Column("email", Order = 3)]
        [EmailAddress]
        public required string Email { get; set; }

        [Column("persona_id", Order = 4)]
        [DefaultValue(null)]
        public int? PersonaId { get; set; }
        
        public Persona? Persona { get; set; }
        [Column("avatar_archivo_id", Order = 5)]
        [DefaultValue(null)]
        public int? AvatarArchivoId { get; set; }
        public Archivo? AvatarArchivo { get; set; }

        [Required]
        [Column("last_login", Order = 6)]
        public DateTime? LastLogin { get; set; }

        [Required]
        [DefaultValue(0)]
        [Column("login_trys", Order = 7)]
        public int LoginTrys { get; set; } = 0;

        [Required]
        [Column("bloqueado", Order = 8)]
        [DefaultValue(false)]
        public bool Bloqueado { get; set; } = false;

        public virtual ICollection<Rol> Roles { get; set; } = new List<Rol>();
    }
}
