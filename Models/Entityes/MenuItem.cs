using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("menu_item")]
    public class MenuItem : GenericEntity
    {
        [Column("Nombre")]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
        [Column("codigo")]
        [Required]
        public required string Codigo { get; set; }
        [Column("url")]
        [Required]
        public required string Url { get; set; }
        [Column("menu_grupo_id")]
        [Required]
        public required int MenuGrupoId { get; set; }
        [Column("icono")]
        [Required]
        [DefaultValue(Icons.Material.Filled.List)]
        public required string? _icono { get; set; }

        public string Icono
        {
            get => (string.IsNullOrEmpty(_icono) || string.IsNullOrWhiteSpace(_icono)) ? Icons.Material.Filled.List : _icono;  
            

            set => _icono = value;
        }

        public required MenuGroup MenuGrupo { get; set; }
    }
}
