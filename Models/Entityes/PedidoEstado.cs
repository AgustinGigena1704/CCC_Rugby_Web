using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("pedido_estado")]
    public class PedidoEstado : GenericEntity
    {
        [Column("nombre", Order = 1)]
        [Required]
        public required string Nombre { get; set; }
        [Column("codigo", Order = 2)]
        [Required]
        public required string Codigo { get; set; }
    }
}
