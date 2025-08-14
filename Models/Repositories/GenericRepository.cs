using CCC_Rugby_Web.Models.Entityes;
using Microsoft.EntityFrameworkCore;

namespace CCC_Rugby_Web.Models.Repositories
{
    public class GenericRepository<T> where T : class, IGenericEntity
    {
        protected readonly CCC_DbContext context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(CCC_DbContext context)
        {
            this.context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
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
            var result = await _dbSet.FindAsync(id);

            return result?.BorradoLogico == false ? result : null;
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
    }
}
