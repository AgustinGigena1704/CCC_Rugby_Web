using CCC_Rugby_Web.Models;
using System.Reflection;
namespace CCC_Rugby_Web.Services
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RepositoryAttribute : Attribute
    {
        public Type EntityType { get; }
        public RepositoryAttribute(Type entityType)
        {
            EntityType = entityType;
        }
    }

    public class EntityManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CCC_DbContext cccDbContext;

        public EntityManager(IServiceProvider serviceProvider, CCC_DbContext cccDbContext)
        {
            _serviceProvider = serviceProvider;
            this.cccDbContext = cccDbContext;
        }

        public TRepo GetRepository<TRepo>() where TRepo : class
        {
            var repoType = typeof(TRepo);

            // Opcional: Validar que el tipo tenga el atributo RepositoryAttribute
            if (repoType.GetCustomAttribute<RepositoryAttribute>() == null)
                throw new InvalidOperationException($"El repositorio {repoType.Name} no tiene RepositoryAttribute.");

            var repo = _serviceProvider.GetService(repoType) as TRepo;
            if (repo == null)
                throw new InvalidOperationException($"No se pudo instanciar el repositorio {repoType.Name}");

            return repo;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await cccDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, logging, etc.
                throw new InvalidOperationException("Error al guardar los cambios en la base de datos.", ex);
            }
        }
    }
}
