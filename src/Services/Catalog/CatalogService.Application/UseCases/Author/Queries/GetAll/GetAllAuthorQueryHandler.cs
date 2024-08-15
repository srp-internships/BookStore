using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases
{
    public class GetAllAuthorQueryHandler(
        IAuthorRepository authorRepository,
        IMapper mapper) : IRequestHandler<GetAllAuthorQuery, List<AuthorDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<List<AuthorDto>> Handle(GetAllAuthorQuery request, CancellationToken token)
        {
            IEnumerable<Author> authors;
            try
            {
                authors = await _authorRepository.GetAllAsync(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (authors.Count() == 0)
            {
                return [];
            }

            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return authorDtos.ToList();
        }
    }
}
