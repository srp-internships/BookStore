﻿using AutoMapper;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.UseCases
{
    public class GetByIdBookSellerQueryHandler(
        IBookSellerRepository sellerRepository,
        IMapper mapper) : IRequestHandler<GetByIdBookSellerQuery, BookSellerDto>
    {
        private readonly IBookSellerRepository _bsRepository = sellerRepository;
        private readonly IMapper _mapper = mapper;


        public async Task<BookSellerDto> Handle(GetByIdBookSellerQuery request, CancellationToken token)
        {
            BookSeller? bookSeller;

            try
            {
                bookSeller = await _bsRepository.GetByIdAsync(request.Id, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (bookSeller == null)
            {
                throw new NotFoundException(nameof(BookSeller), request.Id);
            }
            var bookSellerDto = _mapper.Map<BookSellerDto>(bookSeller);
            return bookSellerDto;
        }
    }
}
