using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infostructure.Repositories
{
    public class PublisherRepository(
        CatalogDbContext dbContext,
        IUnitOfWork unitOfWork) : IPublisherRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Guid> CreateAsync(Publisher publisher, CancellationToken token = default)
        {
            var existingPublisher = await _dbcontext.Publishers.FirstOrDefaultAsync(x => x.Name.Equals(publisher.Name), token);

            if (existingPublisher != null)
            {
                return existingPublisher.Id;
            }
            await _dbcontext.Publishers.AddAsync(publisher, token);
            await _unitOfWork.SaveChangesAsync(token);
            return publisher.Id;
        }

        public Task<Publisher> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _dbcontext.Publishers.FirstOrDefaultAsync(x => x.Id.Equals(id), token); ;
        }

        public Task<List<Publisher>> GetAllAsync(CancellationToken token)
        {
            return _dbcontext.Publishers.ToListAsync(token); 
        }

        public async Task UpdateAsync(Publisher publisher, CancellationToken token = default)
        {
            var entity = await _dbcontext.Publishers.FirstOrDefaultAsync(author
                => author.Id.Equals(publisher.Id), token);
            entity.Name = publisher.Name;
            entity.Address = publisher.Address;
            entity.Logo = publisher.Logo;
            entity.Email = publisher.Email;

            await _unitOfWork.SaveChangesAsync(token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var entity = await _dbcontext.Publishers.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Publisher), entity.Id);
            }
            _dbcontext.Publishers.Remove(entity);
            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
