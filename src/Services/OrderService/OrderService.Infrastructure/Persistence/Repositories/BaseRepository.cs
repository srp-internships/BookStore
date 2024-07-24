using Microsoft.EntityFrameworkCore;
using OrderService.Application.Common.Interfaces.Repositories;
using OrderService.Application.Common.Pagination;


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
        await _dbContext.SaveChangesAsync(token);
        return entity;
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken token = default)
    {
        _dbSet.Remove(entity);
        return await _dbContext.SaveChangesAsync(token) > 0;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(PaginationRequest paginationRequest, CancellationToken token = default)
    {
       throw new NotImplementedException();
    }

    public async Task<TEntity> GetAsync(int id, CancellationToken token = default)
    {
        var entity = await _dbSet.FindAsync(id, token);

        if (entity is null)
            throw new Exception($"Not found entity with the following id: {id}");

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        entity = _dbSet.Update(entity).Entity;
        await _dbContext.SaveChangesAsync(token);
        return entity;
    }
}