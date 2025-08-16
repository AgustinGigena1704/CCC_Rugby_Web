using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("pedido_articulo")]
    public class PedidoArticulo : GenericEntity
    {
        [Column("pedido_id")]
        [Required]
        public required int PedidoId { get; set; }
        public required Pedido Pedido { get; set; }
        [Column("articulo_id")]
        [Required]
        public required int ArticuloId { get; set; }
        public required Articulo Articulo { get; set; }
        [Column("cantidad")]
        [Required]
        public required int Cantidad { get; set; }
        
    }
}
