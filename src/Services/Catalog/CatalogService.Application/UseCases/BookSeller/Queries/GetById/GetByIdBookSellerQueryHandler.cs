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
    public class GetByIdBookSellerQueryHandler(
        IBookSellerRepository sellerRepository,
        IMapper mapper) : IRequestHandler<GetByIdBookSellerQuery, BookSellerDto>
    {
        private readonly IBookSellerRepository _sellerRepository = sellerRepository;
        private readonly IMapper _mapper = mapper;


        public async Task<BookSellerDto> Handle(GetByIdBookSellerQuery request, CancellationToken token)
        {
            var bookSeller = await _sellerRepository.GetByIdAsync(request.Id, token);
            var bookSellerDto = _mapper.Map<BookSellerDto>(bookSeller);
            return bookSellerDto;
        }
    }
}
