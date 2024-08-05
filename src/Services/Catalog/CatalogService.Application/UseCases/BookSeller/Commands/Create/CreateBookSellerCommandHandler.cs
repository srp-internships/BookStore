using AutoMapper;
using CatalogService.Contracts;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using FluentValidation;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreateBookSellerCommandHandler(
        IBookSellerRepository sellerRepository,
        IMapper mapper,
        IValidator<CreateBookSellerCommand> validator,
        IBus bus) : IRequestHandler<CreateBookSellerCommand>
    {
        private readonly IBookSellerRepository _repository = sellerRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateBookSellerCommand> _validator = validator;
        private readonly IBus _bus = bus;

        public async Task Handle(CreateBookSellerCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var bookSeller = _mapper.Map<BookSeller>(request);
            await _repository.CreateAsync(bookSeller, token);

            await _bus.Publish(new PriceCreatedEvent
            {
                BookId = bookSeller.BookId,
                SellerId = bookSeller.SellerId,
                Price = bookSeller.Price,
            });
        }
    }
}
