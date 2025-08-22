namespace CCC_Rugby_Web.DTOs
{
    public class RequestDTO
    {
        public PaginacionDTO? paginacion { get; set; } = null;
        public DateTime inicio { get; set; } = DateTime.UtcNow;
        public DateTime fin { get; set; } = DateTime.UtcNow;

        public RequestDTO(PaginacionDTO? paginacion, DateTime? inicio, DateTime? fin)
        {
            this.paginacion = paginacion;
            if (inicio == null)
            {
                inicio = DateTime.UtcNow;
            }
            this.inicio = (DateTime)inicio;
            if (fin == null)
            {
                fin = DateTime.UtcNow;
            }
            this.fin = (DateTime)fin;
        }
    }
}
