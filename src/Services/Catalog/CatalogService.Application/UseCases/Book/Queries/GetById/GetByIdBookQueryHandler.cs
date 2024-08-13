using AutoMapper;
using CatalogService.Application.UseCases.Queries;
using CatalogService.Domain.Entities;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetByIdBookQueryHandler(
        IBookRepository bookRepository,
        IMapper mapper) : IRequestHandler<GetByIdBookQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BookDto> Handle(GetByIdBookQuery request, CancellationToken token)
        {
            Book book;

            try
            {
                book = await _bookRepository.GetByIdAsync(request.Id, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            var bookDto = _mapper.Map<BookDto>(book);

            return bookDto;

        }
    }
}
