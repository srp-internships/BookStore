using OrderService.Application.Common.Interfaces.Repositories;


namespace OrderService.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken token = default)
    {
        await _dbSet.AddAsync(entity, token);
        return entity;
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken token = default)
    {
        _dbSet.Remove(entity);
        return await _dbContext.SaveChangesAsync(token) > 0;
    }

    public async Task<TEntity?> GetAsync(Guid id, CancellationToken token = default)
    {
        var entity = await _dbSet.FindAsync(id, token);

        if (entity is null)
            return null;

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        entity = _dbSet.Update(entity).Entity;
        await _dbContext.SaveChangesAsync(token);
        return entity;
    }
}