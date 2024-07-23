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
    public class CreateBookCommandHandler(
        IBookRepository bookrepository,
        ICategoryRepository categoryRepository,
        IAuthorRepository authorRepository,
        IValidator<CreateBookCommand> validator,
        IMapper mapper)
        : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IBookRepository _bookrepository = bookrepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateBookCommand> _validator = validator;

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAsync(request, token);

            var categoryList = await _categoryRepository.GetByIdsAsync(request.CategoryIds);
            /*if (!categoryList.Count().Equals(request.CategoryIds.Count))
            {
                throw new NotFoundException(nameof(Category), categoryList); //TODO: to be fixed
            }*/
            var authorList = await _authorRepository.GetByIdsAsync(request.AuthorIds);
            /*if (!authorList.Count().Equals(request.AuthorIds.Count))
            {
                throw new NotFoundException(nameof(Author), authorList);
            }*/

            var book = _mapper.Map<Book>(request);

            book.Authors = authorList.ToArray();
            book.Categories = categoryList.ToArray();
            book.PublisherId = request.PublisherId;
            Guid guid = await _bookrepository.CreateAsync(book, token);
            return guid;

        }
    }
}
