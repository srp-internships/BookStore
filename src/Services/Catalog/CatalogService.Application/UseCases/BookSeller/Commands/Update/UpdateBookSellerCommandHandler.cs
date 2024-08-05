using AutoMapper;
using CatalogService.Contracts;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
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
        IMapper mapper,
        IBus bus) : IRequestHandler<UpdateBookSellerCommand>
    {
        private readonly IBookSellerRepository _sellerRepository = sellerRepository;
        private readonly IValidator<UpdateBookSellerCommand> _validator = validator;
        private readonly IMapper _mapper = mapper;
        private readonly IBus _bus = bus;

        public async Task Handle(UpdateBookSellerCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);

            var bookSeller = _mapper.Map<BookSeller>(request);
            await _sellerRepository.UpdateAsync(bookSeller, token);

            await _bus.Publish(new PriceUpdatedEvent
            {
                BookId = bookSeller.BookId,
                SellerId = bookSeller.SellerId,
                Price = bookSeller.Price,
            });
        }
    }
}
