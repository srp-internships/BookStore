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
    public class UpdateAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IValidator<UpdateAuthorCommand> validator,
        IMapper mapper) : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<UpdateAuthorCommand> _validator = validator;

        public async Task Handle(UpdateAuthorCommand request, CancellationToken token)
        {
            await _validator.ValidateAsync(request, token);

            var author = _mapper.Map<Author>(request);
            await _authorRepository.UpdateAsync(request.Id, author, token);
        }
    }
}
