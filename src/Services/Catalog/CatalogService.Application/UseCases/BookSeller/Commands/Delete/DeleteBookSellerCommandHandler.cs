using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class DeleteBookSellerCommandHandler(
        IBookSellerRepository sellerRepository) : IRequestHandler<DeleteBookSellerCommand>
    {
        private readonly IBookSellerRepository _sellerRepository = sellerRepository;

        public async Task Handle(DeleteBookSellerCommand request, CancellationToken token)
        {
            await _sellerRepository.DeleteAsync(request.Id, token);
        }
    }
}
