using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;
using Microsoft.EntityFrameworkCore;

namespace CCC_Rugby_Web.Models.Repositories
{
    public class GenericRepository<T> where T : class, IGenericEntity
    {
        protected readonly CCC_DbContext context;
        protected readonly DbSet<T> _dbSet;
        protected readonly EntityManager entityManager;

        public GenericRepository(CCC_DbContext context, EntityManager entityManager)
        {
            this.context = context;
            _dbSet = context.Set<T>();
            this.entityManager = entityManager;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = _dbSet.AsQueryable();
            query = IncludeNavigationProperties(query);
            var entities = await query.ToListAsync();
            var result = entities.Where(e =>
            {
                var prop = typeof(T).GetProperty("BorradoLogico");
                if (prop != null && prop.PropertyType == typeof(bool))
                {
                    return e.BorradoLogico;
                }
                return true;
            });
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = _dbSet.AsQueryable();
            result = IncludeNavigationProperties(result);
            var user = await result.FirstOrDefaultAsync(e => e.Id == id);

            return user?.BorradoLogico == false ? user : null;
        }

        public async Task CreateAsync(T entity, Usuario user_by)
        {
            var propby = typeof(T).GetProperty("CreatedBy");
            var propbyUsuario = typeof(T).GetProperty("CreatedByUsuario");
            var propAt = typeof(T).GetProperty("CreatedAt");

            await _dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity, Usuario user_by)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = user_by.Id;
            entity.UpdatedByUsuario = user_by;
            _dbSet.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.BorradoLogico = true;
                entity.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
                await context.SaveChangesAsync();
            }
        }

        protected virtual IQueryable<T> IncludeNavigationProperties(IQueryable<T> query)
        {
            // Obtener el tipo de entidad
            var entityType = context.Model.FindEntityType(typeof(T));
            if (entityType == null) return query;

            // Obtener todas las propiedades de navegación
            var navigationProperties = entityType.GetNavigations()
                .Where(n => !n.IsOnDependent) // Solo propiedades de navegación principales
                .ToList();

            // Incluir cada propiedad de navegación
            foreach (var navigation in navigationProperties)
            {
                query = query.Include(navigation.Name);
            }

            return query;
        }
    }
}



