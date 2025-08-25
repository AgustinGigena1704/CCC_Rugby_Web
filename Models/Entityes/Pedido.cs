using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("pedido")]
    public class Pedido : GenericEntity
    {
        [Column("usuario_id", Order = 1)]
        [Required]
        public required int UsuarioId { get; set; }
        public required Usuario Usuario { get; set; }
        [Column("fecha", Order = 2)]
        [Required]
        public required DateTime Fecha { get; set; }
        [Column("estado_id", Order = 3)]
        [Required]
        public required int EstadoId { get; set; } = 1;
        public required PedidoEstado Estado { get; set; }
        [Column("tipo_pago_id", Order = 4)]
        public int? TipoPagoId { get; set; }
        public TipoPago? TipoPago { get; set; }
        [Column("nombre_comprador", Order = 5)]
        [Required]
        public required string NombreComprador { get; set; }
        [Column("direccion_entrega", Order = 6)]
        public string? DireccionEntrega { get; set; } = null;

        public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
    }
}
