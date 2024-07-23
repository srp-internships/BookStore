using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
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
            var book = await _bookRepository.GetByIdAsync(request.Id, token);
            var bookDto = _mapper.Map<BookDto>(book);

            return bookDto;

        }
    }
}
