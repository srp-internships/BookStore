using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreateAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IValidator<CreateAuthorCommand> validator,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateAuthorCommand, Guid>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateAuthorCommand> _validator = validator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken token)
        {
            await _validator.ValidateAsync(request, token);

            var author = _mapper.Map<Author>(request);
            Guid guid = await _authorRepository.CreateAsync(author, token);
            _unitOfWork.SaveChangesAsync();
            return guid;
        }
    }
}
