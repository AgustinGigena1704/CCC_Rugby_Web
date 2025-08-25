using CCC_Rugby_Web.Models.Entityes;

namespace CCC_Rugby_Web.DTOs
{
    public class PedidoDTO
    {
        public required int Id { get; set; }
        public required int CantidadArticulos { get; set; } = 0;
        public required int EstadoId { get; set; }
        public required string EstadoNombre { get; set; }
        public string? TipoPagoNombre { get; set; } = null;
        public int? TipoPagoId { get; set; } = null;
        public required string NombreComprador { get; set; }
        public required string UsuarioUsername { get; set; }
        public string? DireccionEntrega { get; set; } = null;
        public required DateTime Fecha { get; set; }
    }
}
