using AutoMapper;
using CatalogService.Application.Exceptions;
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
    public class UpdateAuthorsBookCommandHandler(
        IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        IValidator<UpdateAuthorsBookCommand> validator) : IRequestHandler<UpdateAuthorsBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IValidator<UpdateAuthorsBookCommand> _validator = validator;
        public async Task Handle(UpdateAuthorsBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var authors = await _authorRepository.GetByIdsAsync(request.AuthorIds, token);
            await _bookRepository.UpdateAuthorsAsync(request.Id, authors, token);
        }
    }
}
