using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;

namespace CCC_Rugby_Web.Models.Repositories
{
    [Repository(typeof(Archivo))]
    public class ArchivoRepository : GenericRepository<Archivo>
    {
        public ArchivoRepository(CCC_DbContext context, EntityManager entity) : base(context, entity)
        {
        }
    }
}
