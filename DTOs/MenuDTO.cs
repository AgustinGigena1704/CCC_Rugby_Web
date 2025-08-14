namespace CCC_Rugby_Web.DTOs
{
    public class MenuDTO
    {
        public string Nombre { get; set; }
        public string? Descripcion { get; set; } = null;
        public List<MenuGroupDTO> MenuGrupos { get; set; } = new List<MenuGroupDTO>();
    }
}
