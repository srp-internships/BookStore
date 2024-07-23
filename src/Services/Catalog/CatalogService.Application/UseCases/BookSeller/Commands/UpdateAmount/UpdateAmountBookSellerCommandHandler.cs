using AutoMapper;
using CatalogService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdateAmountBookSellerCommandHandler(
        IBookSellerRepository sellerRepository,
        IValidator<UpdateAmountBookSellerCommand> validator) : IRequestHandler<UpdateAmountBookSellerCommand>
    {
        private readonly IBookSellerRepository _sellerRepository = sellerRepository;
        private readonly IValidator<UpdateAmountBookSellerCommand> _validator = validator;

        public async Task Handle(UpdateAmountBookSellerCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            await _sellerRepository.UpdateAmountAsync(request.Id, request.Amount, token);
        }
    }
}
