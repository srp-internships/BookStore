using AutoMapper;
using CatalogService.Domain.Entities;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.UnitOfWork;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IValidator<CreateCategoryCommand> validator,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IValidator<CreateCategoryCommand> _validator = validator;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken token)
        {
            await _validator.ValidateAsync(request, token);

            var category = _mapper.Map<Category>(request);
            Guid guid = await _categoryRepository.CreateAsync(category, token);
            _unitOfWork.SaveChangesAsync();
            return guid;
        }
    }
}
