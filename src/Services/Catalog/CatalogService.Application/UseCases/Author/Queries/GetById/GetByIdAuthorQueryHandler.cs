using AutoMapper;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases
{
    public class GetByIdAuthorQueryHandler(
        IAuthorRepository authorRepository,
        IMapper mapper) : IRequestHandler<GetByIdAuthorQuery, AuthorDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<AuthorDto> Handle(GetByIdAuthorQuery request, CancellationToken token)
        {
            Author author;

            try
            {
                author = await _authorRepository.GetByIdAsync(request.Id, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (author == null)
            {
                throw new NotFoundException(nameof(Author), request.Id);
            }

            return _mapper.Map<AuthorDto>(author);
        }
    }
}
