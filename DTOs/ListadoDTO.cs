namespace CCC_Rugby_Web.DTOs
{
    public class ListadoDTO<T> where T : class
    {
        public int TotalItems { get; set; }
        public List<T> Listado { get; set; } = new List<T>();
    }
}
