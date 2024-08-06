using AutoMapper;
using CatalogService.Contracts;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using CatalogService.Infostructure;
using FluentValidation;
using MassTransit;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdateBookSellerCommandHandler(
        IBookSellerRepository sellerRepository,
        IValidator<UpdateBookSellerCommand> validator,
        IBus bus,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateBookSellerCommand>
    {
        private readonly IBookSellerRepository _sellerRepository = sellerRepository;
        private readonly IValidator<UpdateBookSellerCommand> _validator = validator;
        private readonly IBus _bus = bus;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(UpdateBookSellerCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);

            var bookSeller = await _sellerRepository.GetByIdAsync(request.Id, token);
            bookSeller.Price = request.Price;
            bookSeller.Description = request.Description;
            await _unitOfWork.SaveChangesAsync();

            await _bus.Publish(new PriceUpdatedEvent
            {
                BookId = bookSeller.BookId,
                SellerId = bookSeller.SellerId,
                Price = bookSeller.Price,
            });
        }
    }
}
