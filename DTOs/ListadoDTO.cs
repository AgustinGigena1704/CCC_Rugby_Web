namespace CCC_Rugby_Web.DTOs
{
    public class ListadoDTO<T> where T : class
    {
        public int TotalItems;
        public List<T> Listado = new List<T>();
    }
}
