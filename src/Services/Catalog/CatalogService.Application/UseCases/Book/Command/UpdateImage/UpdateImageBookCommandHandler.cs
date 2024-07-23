using AutoMapper;
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
    public class UpdateImageBookCommandHandler(
        IBookRepository bookRepository,
        IValidator<UpdateImageBookCommand> validator) : IRequestHandler<UpdateImageBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IValidator<UpdateImageBookCommand> _validator = validator;

        public async Task Handle(UpdateImageBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            await _bookRepository.UpdateImageAsync(request.Id, request.Image, token);
        }
    }
}
