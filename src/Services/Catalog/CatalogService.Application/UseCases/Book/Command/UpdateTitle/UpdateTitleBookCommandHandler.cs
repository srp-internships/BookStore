
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
    public class UpdateTitleBookCommandHandler(
        IBookRepository bookRepository,
        IValidator<UpdateTitleBookCommand> validator) : IRequestHandler<UpdateTitleBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IValidator<UpdateTitleBookCommand> _validator = validator;

        public async Task Handle(UpdateTitleBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            await _bookRepository.UpdateImageAsync(request.Id, request.Title, token);
        }
    }
}
