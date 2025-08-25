namespace CCC_Rugby_Web.DTOs
{
    public class RequestDTO
    {
        public PaginacionDTO? paginacion { get; set; } = null;
        public DateTime inicio { get; set; } = DateTime.UtcNow;
        public DateTime fin { get; set; } = DateTime.UtcNow;

        // Constructor sin parámetros OBLIGATORIO para deserialización JSON
        public RequestDTO() { }

        // Tu constructor con parámetros (mantén este si lo necesitas)
        public RequestDTO(PaginacionDTO? paginacion, DateTime? inicio, DateTime? fin)
        {
            this.paginacion = paginacion;
            this.inicio = inicio ?? DateTime.UtcNow;
            this.fin = fin ?? DateTime.UtcNow;
        }
    }
}