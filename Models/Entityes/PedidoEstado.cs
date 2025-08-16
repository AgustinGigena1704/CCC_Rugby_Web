using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    [Table("pedido_estado")]
    public class PedidoEstado
    {
        [Column("nombre")]
        [Required]
        public required string Nombre { get; set; }
        [Column("codigo")]
        [Required]
        public required string Codigo { get; set; }
    }
}
