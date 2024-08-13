using AutoMapper;
using CatalogService.Application.UseCases.Queries;
using CatalogService.Contracts;
using CatalogService.Domain.Entities;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.BlobServices;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
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
    public class CreateBookCommandHandler(
        IBookRepository bookrepository,
        ICategoryRepository categoryRepository,
        IAuthorRepository authorRepository,
        IValidator<CreateBookCommand> validator,
        IMapper mapper,
        IBus bus,
        IUnitOfWork unitOfWork,
        IBlobService blobService) : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IBookRepository _bookrepository = bookrepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateBookCommand> _validator = validator;
        private readonly IBus _bus = bus;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBlobService _blobService = blobService;

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAsync(request, token);

            var categoryList = await _categoryRepository.GetByIdsAsync(request.CategoryIds, token);
            if (!categoryList.Count().Equals(request.CategoryIds.Count))
            {
                throw new NotFoundException(nameof(Category)); 
            }
            var authorList = await _authorRepository.GetByIdsAsync(request.AuthorIds, token);
            if (!authorList.Count().Equals(request.AuthorIds.Count))
            {
                throw new NotFoundException(nameof(Author));
            }

            if (request.Image == null)
                throw new NoFileUploaded();

            var uri = await _blobService.UploadAsync(request.Image.Content, request.Image.ContentType, token);

            var book = _mapper.Map<Book>(request);

            book.Authors = authorList;
            book.Categories = categoryList;
            book.PublisherId = request.PublisherId;
            book.Image = uri;
            Guid guid = await _bookrepository.CreateAsync(book, token);
            await _unitOfWork.SaveChangesAsync();

            await _bus.Publish(new BookCreatedEvent
            {
                Id = guid,
                Title = request.Title,
                Image = uri,
                AuthorIds = request.AuthorIds.ToList(),
                CategoryIds = request.CategoryIds.ToList()
            });

            return guid;

        }
    }
}
