using CCC_Rugby_Web.Models.Entityes;
using CCC_Rugby_Web.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CCC_Rugby_Web.Models.Repositories
{
    public class GenericRepository<T> where T : class, IGenericEntity
    {
        protected readonly CCC_DbContext context;
        protected readonly DbSet<T> _dbSet;
        protected readonly EntityManager entityManager;
        protected readonly IUserContextService userContextService;

        public GenericRepository(CCC_DbContext context, EntityManager entityManager, IUserContextService userContextService)
        {
            this.context = context;
            _dbSet = context.Set<T>();
            this.entityManager = entityManager;
            this.userContextService = userContextService;
        }

        public async Task<Usuario?> GetActualUserAsync()
        {
            return await userContextService.GetActualUserAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var e = await _dbSet.FindAsync(id);
            if (e == null) return null;
            if (e.BorradoLogico == true) return null;
            return e;
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || includes.Length == 0)
            {
                return await GetByIdAsync(id);
            }

            var query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var entity = await query.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null) return null;
            if (entity.BorradoLogico == true) return null;
            return entity;
        }

        public async Task CreateAsync(T entity)
        {
            var user_by = await GetActualUserAsync();
            if (user_by != null)
            {
                var propby = typeof(T).GetProperty("CreatedBy");
                var propbyUsuario = typeof(T).GetProperty("CreatedByUsuario");
                var propAt = typeof(T).GetProperty("CreatedAt");
                if (propby == null || propAt == null || propbyUsuario == null)
                {
                    throw new Exception("La entidad no tiene las propiedades necesarias para asignar CreatedBy, CreatedAt o CreatedByUsuario");
                }
                propby.SetValue(entity, user_by.Id);
                propbyUsuario.SetValue(entity, user_by);
                propAt.SetValue(entity, DateTime.UtcNow);
                await _dbSet.AddAsync(entity);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No se pudo obtener el usuario actual para asignar CreatedBy");
            }
        }

        public async Task UpdateAsync(T entity)
        {
            var user_by = await GetActualUserAsync();
            entity.UpdatedAt = DateTime.UtcNow;
            if (user_by != null)
            {
                entity.UpdatedBy = user_by.Id;
                entity.UpdatedByUsuario = user_by;
            }
            _dbSet.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            var user_by = await GetActualUserAsync();
            entity.BorradoLogico = true;
            entity.DeletedAt = DateTime.UtcNow;
            if (user_by != null)
            {
                entity.DeletedBy = user_by.Id;
                entity.DeletedByUsuario = user_by;
            }
            _dbSet.Update(entity);
            await context.SaveChangesAsync();
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



