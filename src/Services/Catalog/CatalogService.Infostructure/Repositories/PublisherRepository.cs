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
        CatalogDbContext dbContext) : IPublisherRepository
    {
        private readonly CatalogDbContext _dbcontext = dbContext;

        public async Task<Guid> CreateAsync(Publisher publisher, CancellationToken token = default)
        {
            var existingPublisher = await _dbcontext.Publishers.FirstOrDefaultAsync(x => x.Name.Equals(publisher.Name), token);

            if (existingPublisher != null)
            {
                return existingPublisher.Id;
            }
            else
            {
                await _dbcontext.Publishers.AddAsync(publisher, token);
                await _dbcontext.SaveChangesAsync(token);
                return publisher.Id;
            }
        }
        public async Task<Publisher> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var author = await _dbcontext.Publishers.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            return author;
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync(CancellationToken token)
        {
            var publisherlist = await _dbcontext.Publishers.ToListAsync(token);
            return publisherlist;
        }

        public async Task UpdateAsync(Guid id, Publisher publisher, CancellationToken token = default)
        {
            Publisher entity = await _dbcontext.Publishers.FirstOrDefaultAsync(author
                => author.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Publisher), publisher.Id);
            }
            entity.Name = publisher.Name;
            entity.Address = publisher.Address;
            entity.Logo = publisher.Logo;
            entity.Email = publisher.Email;

            await _dbcontext.SaveChangesAsync(token);
        }
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            Publisher entity = await _dbcontext.Publishers.FirstOrDefaultAsync(x => x.Id.Equals(id), token);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Publisher), entity.Id);
            }
            _dbcontext.Publishers.Remove(entity);
            await _dbcontext.SaveChangesAsync(token);
        }
    }
}
