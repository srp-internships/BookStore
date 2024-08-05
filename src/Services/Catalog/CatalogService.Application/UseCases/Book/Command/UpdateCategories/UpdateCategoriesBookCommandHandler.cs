using AutoMapper;
using CatalogService.Contracts;
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
    public class UpdateCategoriesBookCommandHandler(
        IBookRepository bookRepository,
        ICategoryRepository categoryRepository,
        IValidator<UpdateCategoriesBookCommand> validator,
        IBus bus) : IRequestHandler<UpdateCategoriesBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IValidator<UpdateCategoriesBookCommand> _validator = validator;
        private readonly IBus _bus = bus;
        public async Task Handle(UpdateCategoriesBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var categories = await _categoryRepository.GetByIdsAsync(request.CategoryIds, token);
            await _bookRepository.UpdateCategoriesAsync(request.Id, categories, token);
            var book = await _bookRepository.GetByIdAsync(request.Id, token);

            List<Guid> authorIds = book.Authors.Select(p => p.Id).ToList();
            await _bus.Publish(new BookUpdatedEvent
            {
                Id = book.Id,
                Title = book.Title,
                Image = book.Image,
                CategoryIds = request.CategoryIds.ToList(),
                AuthorIds = authorIds
            });
        }
    }
}
