using CCC_Rugby_Web.DTOs;

namespace CCC_Rugby_Web.Components.Utils
{
    public interface IUtilities
    {
        ValidacionDTO ValidarFechas(DateTime? t1, DateTime? t2);
    }
    public class Utilities : IUtilities
    {
        public ValidacionDTO ValidarFechas(DateTime? t1, DateTime? t2)
        {
            if(t1 == null || t2 == null)
            {
                return new ValidacionDTO("Las Fechas no pueden ser nulas", false);
            }

            if(t2 < t1)
            {
                return new ValidacionDTO("La Fecha de fin debe ser luego de la fecha de inicio", false);

            }

            return new ValidacionDTO(null, true);
        }
    }
}
