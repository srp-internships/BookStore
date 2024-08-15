using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.UseCases.Queries;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases
{
    public class GetAllBookQueryHandler(
        IBookRepository bookRepository,
        IMapper mapper) : IRequestHandler<GetAllBookQuery, List<BookDto>>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<BookDto>> Handle(GetAllBookQuery request, CancellationToken token)
        {
            List<Book> books;
            try
            {
                books = await _bookRepository.GetAllAsync(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (books.Count == 0)
            {
                return [];
            }

            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return bookDtos.ToList();
        }
    }
}
