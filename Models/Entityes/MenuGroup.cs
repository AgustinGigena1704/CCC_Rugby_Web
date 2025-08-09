using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("menu_grupo")]
    public class MenuGroup : GenericEntity
    {
        [Column("nombre")]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion")]
        [Required]
        public required string Descripcion { get; set; }
        [Column("rol_id")]
        [Required]
        public int RolId { get; set; }
        [Column("icono")]
        [Required]
        [DefaultValue(Icons.Material.Filled.List)]
        public required string? _icono { get; set; }

        public string Icono
        {
            get => (string.IsNullOrEmpty(_icono) || string.IsNullOrWhiteSpace(_icono)) ? Icons.Material.Filled.List : _icono;


            set => _icono = value;
        }

        public required Role Rol { get; set; }
    }
}
