using CatalogService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdatePriceBookSellerCommandHandler(
        IBookSellerRepository sellerRepository,
        IValidator<UpdatePriceBookSellerCommand> validator) : IRequestHandler<UpdatePriceBookSellerCommand>
    {
        private readonly IBookSellerRepository _sellerRepository = sellerRepository;
        private readonly IValidator<UpdatePriceBookSellerCommand> _validator = validator;

        public async Task Handle(UpdatePriceBookSellerCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            await _sellerRepository.UpdatePriceAsync(request.Id, request.Price, token);
        }
    }
}
