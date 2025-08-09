namespace CCC_Rugby_Web.DTOs
{
    public class MenuGroupDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;
        public List<MenuItemDTO> MenuItems { get; set; } = new List<MenuItemDTO>();
    }
}
