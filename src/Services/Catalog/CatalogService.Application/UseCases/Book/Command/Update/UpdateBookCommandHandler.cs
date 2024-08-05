
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
    public class UpdateBookCommandHandler(
        IBookRepository bookRepository,
        IValidator<UpdateBookCommand> validator,
        IMapper mapper,
        IBus bus) : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IValidator<UpdateBookCommand> _validator = validator;
        private readonly IMapper _mapper = mapper;
        private readonly IBus _bus = bus;

        public async Task Handle(UpdateBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var book = _mapper.Map<Book>(request);
            await _bookRepository.UpdateAsync(book, token);

            List<Guid> authorIds = new List<Guid>();
            foreach (var author in book.Authors)
            {
                authorIds.Add(author.Id);
            }
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
                AuthorIds = authorIds
            });
        }
    }
}
