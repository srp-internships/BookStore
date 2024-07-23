
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
    public class UpdatePublisherBookCommandHandler(
        IBookRepository bookRepository,
        IPublisherRepository publisherRepository,
        IValidator<UpdatePublisherBookCommand> validator) : IRequestHandler<UpdatePublisherBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IPublisherRepository _publisherRepository = publisherRepository;
        private readonly IValidator<UpdatePublisherBookCommand> _validator = validator;
        public async Task Handle(UpdatePublisherBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
           
            await _bookRepository.UpdatePublisherAsync(request.Id, request.PublisherId, token);
        }
    }
}
