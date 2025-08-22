namespace CCC_Rugby_Web.DTOs
{
    public class ValidacionDTO
    {
        public string? Mensaje { get; set; }
        public bool EsValido { get; set; } = true;

        public ValidacionDTO(string? mensaje = null, bool esValido = true)
        {
            Mensaje = mensaje;
            EsValido = esValido;
        }
    }
}
