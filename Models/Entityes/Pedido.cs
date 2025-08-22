using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("pedido")]
    public class Pedido : GenericEntity
    {
        [Column("usuario_id")]
        [Required]
        public required int UsuarioId { get; set; }
        public required Usuario Usuario { get; set; }
        [Column("fecha")]
        [Required]
        public required DateTime Fecha { get; set; }
        [Column("estado")]
        [Required]
        public required PedidoEstado Estado { get; set; }
        [Column("tipo_pago_id")]
        public int? TipoPagoId { get; set; }
        public TipoPago? TipoPago { get; set; }
        [Column("nombre_comprador")]
        [Required]
        public required string NombreComprador { get; set; }
        [Column("direccion_entrega")]
        public string? DireccionEntrega { get; set; } = null;

        public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
    }
}
