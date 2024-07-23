
using AutoMapper;
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
    public class UpdateBookCommandHandler(
        IBookRepository bookRepository,
        IValidator<UpdateBookCommand> validator,
        IMapper mapper) : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IValidator<UpdateBookCommand> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(UpdateBookCommand request, CancellationToken token)
        {
            await _validator.ValidateAndThrowAsync(request, token);
            var book = _mapper.Map<Book>(request);
            await _bookRepository.UpdateAsync(book, token);
        }
    }
}
