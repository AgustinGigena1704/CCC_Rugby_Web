using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("menu_grupo")]
    public class MenuGroup : GenericEntity
    {
        [Column("nombre", Order = 1)]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion", Order = 2)]
        [Required]
        public required string Descripcion { get; set; }
        [Column("menu_id", Order = 3)]
        [Required]
        public int MenuId { get; set; }
        [Column("rol_id", Order = 4)]
        [DefaultValue(null)]
        public int? RolId { get; set; }
        [Column("icono", Order = 5)]
        [Required]
        [DefaultValue(Icons.Material.Filled.List)]
        public required string? _icono { get; set; }

        [NotMapped]
        public string Icono
        {
            get => (string.IsNullOrEmpty(_icono) || string.IsNullOrWhiteSpace(_icono)) ? Icons.Material.Filled.List : _icono;


            set => _icono = value;
        }

        public required Role Rol { get; set; }
        public required Menu Menu { get; set; }
    }
}
