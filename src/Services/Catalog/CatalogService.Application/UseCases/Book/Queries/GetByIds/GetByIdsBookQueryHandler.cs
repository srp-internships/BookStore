using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Application.Exceptions;
using CatalogService.Application.UseCases;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application
{
    public class GetByIdsBookQueryHandler(
        IBookRepository bookRepository,
        IMapper mapper) : IRequestHandler<GetByIdsBookQuery, BookListVm>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BookListVm> Handle(GetByIdsBookQuery request, CancellationToken token)
        {
            IEnumerable<Book> books;
            try
            {
                books = await _bookRepository.GetByIdsAsync(request.BookIds, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (books.Count() == 0)
            {
                throw new NotFoundException(nameof(Book));
            }
            
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return new BookListVm { Books = bookDtos.ToList() };
        }
    }
}
