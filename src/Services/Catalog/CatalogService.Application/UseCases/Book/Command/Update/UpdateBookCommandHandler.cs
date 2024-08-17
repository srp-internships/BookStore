﻿
using AutoMapper;
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
    public class UpdateBookCommandHandler(
        IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        ICategoryRepository categoryRepository,
        IValidator<UpdateBookCommand> validator,
        IBus bus,
        IUnitOfWork unitOfWork,
        IBlobService blobService) : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IValidator<UpdateBookCommand> _validator = validator;
        private readonly IBus _bus = bus;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBlobService _blobService = blobService;

        public async Task Handle(UpdateBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);

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

            var book = await _bookRepository.GetByIdAsync(request.BookId, token);
            book.Title = request.Title;
            book.Image = uri;
            book.Authors = authorList;
            book.Categories = categoryList;
            book.PublisherId = request.PublisherId;

            await _unitOfWork.SaveChangesAsync();


            await _bus.Publish(new BookUpdatedEvent
            {
                Id = book.Id,
                Title = book.Title,
                Image = book.Image,
                CategoryIds = request.CategoryIds.ToList(),
                AuthorIds = request.AuthorIds.ToList()
            });
        }
    }
}
