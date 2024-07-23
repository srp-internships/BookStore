using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetByIdCategoryQueryHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper) : IRequestHandler<GetByIdCategoryQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<CategoryDto> Handle(GetByIdCategoryQuery request, CancellationToken token)
        {
            Category category = await _categoryRepository.GetByIdAsync(request.Id, token);

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
