using AutoMapper;

using CatalogService.Domain.Entities;
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
    public class CreateBookSellerCommandHandler(
        IBookSellerRepository sellerRepository,
        IMapper mapper,
        IValidator<CreateBookSellerCommand> validator ) : IRequestHandler<CreateBookSellerCommand, Guid>
    {
        private readonly IBookSellerRepository _repository = sellerRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateBookSellerCommand> _validator = validator;

        public async Task<Guid> Handle(CreateBookSellerCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token );
            var bookSeller = _mapper.Map<BookSeller>(request);
            return await _repository.CreateAsync(bookSeller, token);
        }
    }
}
