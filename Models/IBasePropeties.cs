using CCC_Rugby_Web.Models.Entityes;

namespace CCC_Rugby_Web.Models
{
    public interface IBasePropeties
    {
        int Id { get; set; }
        DateTime UpdatedAt { get; set; }
        int UpdatedBy { get; set; }
        Usuario? UpdatedByUsuario { get; set; } 
        bool BorradoLogico { get; set; }
        DateTime? DeletedAt { get; set; }
        int? DeletedBy { get; set; }
        Usuario? DeletedByUsuario { get; set; }

    }
}
