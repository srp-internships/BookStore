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
    public class UpdateDescriptionBookSellerCommandHandler(
        IBookSellerRepository sellerRepository,
        IValidator<UpdateDescriptionBookSellerCommand> validator) : IRequestHandler<UpdateDescriptionBookSellerCommand>
    {
        private readonly IBookSellerRepository _sellerRepository = sellerRepository;
        private readonly IValidator<UpdateDescriptionBookSellerCommand> _validator = validator;

        public async Task Handle(UpdateDescriptionBookSellerCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            await _sellerRepository.UpdateDescriptionAsync(request.Id, request.Description, token);
        }
    }
}
