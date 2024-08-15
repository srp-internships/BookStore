using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Contracts;
using CatalogService.Domain.Entities;
using FluentValidation;
using MassTransit;
using MediatR;

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
            // TODO same thing as CreateBookSellerCommandHandler
            await _validator.ValidateAndThrowAsync(request, token);

            var bookSeller = await _sellerRepository.GetByIdAsync(request.Id, token);
            if (bookSeller == null)
            {
                throw new NotFoundException(nameof(BookSeller));
            }

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
