using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repositories
{
    public class SqlRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;

        private readonly bool _useHiLoGenerators;

        protected SqlRepository(DbSet<TEntity> entities, bool useHiLoGenerators = false)
        {
            _entities = entities;
            _useHiLoGenerators = useHiLoGenerators;
        }

        public virtual async Task Add(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            if (_useHiLoGenerators)
            {
                await _entities.AddAsync(entity, token);
            }
            else
            {
                _entities.Add(entity);
            }
        }

        public virtual Task AddRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entities, "entities");
            var entities2 = (entities as List<TEntity>) ?? entities.ToList();
            if (_useHiLoGenerators)
            {
                return _entities.AddRangeAsync(entities2, token);
            }

            _entities.AddRange(entities2);
            return Task.CompletedTask;
        }

        public virtual Task Remove(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            _entities.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task Update(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            _entities.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entities, "entities");
            _entities.UpdateRange(entities);
            return Task.CompletedTask;
        }
    }
}
