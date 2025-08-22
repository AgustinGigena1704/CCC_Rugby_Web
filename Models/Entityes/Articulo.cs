using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("articulos")]
    public class Articulo : GenericEntity
    {
        [Column("nombre")]
        [Required]
        public required string Nombre { get; set; }
        [Column("descripcion")]
        public string? Descripcion { get; set; }
        [Column("codigo")]
        [Required]
        public required string Codigo { get; set; }
        [Column("tipo_articulo_id")]
        [Required]
        public required int TipoArticuloId { get; set; }
        public required TipoArticulo TipoArticulo { get; set; }
        [Column("precio")]
        [Required]
        public decimal Precio { get; set; }
        [Column("archivo_imagen_id")]
        public int? ArchivoImagenId { get; set; }
        public Archivo? ArchivoImagen { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
