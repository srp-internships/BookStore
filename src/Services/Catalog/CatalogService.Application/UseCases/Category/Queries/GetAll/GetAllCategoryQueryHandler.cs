using AutoMapper;
using CatalogService.Application.Dto;

using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{ 
    public class GetAllCategoryQueryHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper) : IRequestHandler<GetAllCategoryQuery, CategoryListVm>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<CategoryListVm> Handle(GetAllCategoryQuery request, CancellationToken token)
        {
            var categories = _categoryRepository.GetAllAsync(token);
            var categoriesVm = _mapper.Map<CategoryListVm>(categories);
            return categoriesVm;
        }
    }
}
