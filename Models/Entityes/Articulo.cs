using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("articulos")]
    public class Articulo : GenericEntity
    {
        [Column("nombre", Order = 1)]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion", Order = 2)]
        public string? Descripcion { get; set; }
        [Column("codigo", Order = 3)]
        [Required]
        public required string Codigo { get; set; }
        [Column("tipo_articulo_id", Order = 4)]
        [Required]
        public required int TipoArticuloId { get; set; }
        public required TipoArticulo TipoArticulo { get; set; }
        [Column("precio", Order = 5)]
        [Required]
        public decimal Precio { get; set; }
        [Column("archivo_imagen_id", Order = 6)]
        public int? ArchivoImagenId { get; set; }
        public Archivo? ArchivoImagen { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
