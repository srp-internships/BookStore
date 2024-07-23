using AutoMapper;
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
            var books = _bookRepository.GetAllAsync(token);
            var booksVm = _mapper.Map<BookListVm>(books);
            return booksVm;
        }
    }
}
