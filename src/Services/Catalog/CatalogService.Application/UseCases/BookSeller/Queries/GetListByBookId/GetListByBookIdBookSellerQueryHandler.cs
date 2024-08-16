using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetListByBookIdBookSellerQueryHandler(
        IBookSellerRepository sellerRepository,
        IMapper mapper) : IRequestHandler<GetListByBookIdBookSellerQuery, List<BookSellerDto>>
    {
        private readonly IBookSellerRepository _bsRepository = sellerRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<BookSellerDto>> Handle(GetListByBookIdBookSellerQuery request, CancellationToken token = default)
        {
            List<BookSeller> bookSellers;
            try
            {
                bookSellers = await _bsRepository.GetListByBookIdAsync(request.BookId, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (bookSellers.Count == 0)
            {
                return [];
            }

            var bookDtos = _mapper.Map<IEnumerable<BookSellerDto>>(bookSellers);
            return bookDtos.ToList();
        }
    }
}
