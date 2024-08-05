using AutoMapper;
using CatalogService.Application.Exceptions;
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
    public class UpdateAuthorsBookCommandHandler(
        IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        IValidator<UpdateAuthorsBookCommand> validator,
        IBus bus) : IRequestHandler<UpdateAuthorsBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IValidator<UpdateAuthorsBookCommand> _validator = validator;
        private readonly IBus _bus = bus;
        public async Task Handle(UpdateAuthorsBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var authors = await _authorRepository.GetByIdsAsync(request.AuthorIds, token);
            await _bookRepository.UpdateAuthorsAsync(request.Id, authors, token);
            var book = await _bookRepository.GetByIdAsync(request.Id, token);

            List<Guid> categoryIds = new List<Guid>();
            foreach (var category in book.Categories)
            {
                categoryIds.Add(category.Id);
            }
            await _bus.Publish(new BookUpdatedEvent
            {
                Id = book.Id,
                Title = book.Title,
                Image = book.Image,
                CategoryIds = categoryIds,
                AuthorIds = request.AuthorIds.ToList()
            });
        }
    }
}
