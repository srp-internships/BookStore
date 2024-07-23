using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetAllBookQueryHandler(
        IBookRepository bookRepository,
        IMapper mapper) : IRequestHandler<GetAllBookQuery, BookListVm>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BookListVm> Handle(GetAllBookQuery request, CancellationToken token)
        {
            var books = await _bookRepository.GetAllAsync(token);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return new BookListVm { Books = bookDtos.ToList() };
        }
    }
}
