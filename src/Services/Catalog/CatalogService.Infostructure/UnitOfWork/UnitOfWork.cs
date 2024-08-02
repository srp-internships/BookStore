using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure
{
    public class UnitOfWork(
        CatalogDbContext dbContext) : IUnitOfWork 
    {
        private readonly CatalogDbContext _dbContext = dbContext;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
