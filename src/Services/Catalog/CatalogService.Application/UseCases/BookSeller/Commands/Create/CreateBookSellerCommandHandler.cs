using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Contracts;
using CatalogService.Domain.Entities;
using FluentValidation;
using MassTransit;
using MediatR;

namespace CatalogService.Application.UseCases
{
    public class CreateBookSellerCommandHandler(
        IBookSellerRepository sellerRepository,
        IMapper mapper,
        IValidator<CreateBookSellerCommand> validator,
        IBus bus,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateBookSellerCommand>
    {
        private readonly IBookSellerRepository _repository = sellerRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateBookSellerCommand> _validator = validator;
        private readonly IBus _bus = bus;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(CreateBookSellerCommand request, CancellationToken token)
        {
            // TODO check that this code runs mediator validation second time the remove it
            await _validator.ValidateAndThrowAsync(request, token);
            var existingBookSeller = await _repository.GetByTwinId(request.BookId, request.SellerId, token);
            if (existingBookSeller != null)
            {
                throw new ArgumentException("You already have this kind of book in your shop!");
            }

            var bookSeller = _mapper.Map<BookSeller>(request);
            await _repository.CreateAsync(bookSeller, token);
            await _unitOfWork.SaveChangesAsync();

            await _bus.Publish(new PriceCreatedEvent
            {
                BookId = bookSeller.BookId,
                SellerId = bookSeller.SellerId,
                Price = bookSeller.Price,
            });
        }
    }
}
