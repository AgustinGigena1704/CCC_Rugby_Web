using MudBlazor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("menu_item")]
    public class MenuItem : GenericEntity
    {
        [Column("nombre", Order = 1)]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion", Order = 2)]
        public string Descripcion { get; set; } = string.Empty;
        [Column("codigo", Order = 3)]
        [Required]
        public required string Codigo { get; set; }
        [Column("url", Order = 4)]
        [Required]
        public required string Url { get; set; }
        [Column("menu_grupo_id", Order = 5)]
        [Required]
        public required int MenuGrupoId { get; set; }
        [Column("icono", Order = 6)]
        [Required]
        [DefaultValue(Icons.Material.Filled.List)]
        public required string? _icono { get; set; }

        [NotMapped]
        public string Icono
        {
            get => (string.IsNullOrEmpty(_icono) || string.IsNullOrWhiteSpace(_icono)) ? Icons.Material.Filled.List : _icono;

            set => _icono = value;
        }

        public required MenuGroup MenuGrupo { get; set; }
    }
}
