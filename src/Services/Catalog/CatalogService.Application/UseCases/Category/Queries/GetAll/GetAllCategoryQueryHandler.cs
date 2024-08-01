using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Application.Exceptions;
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
    public class GetAllCategoryQueryHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper) : IRequestHandler<GetAllCategoryQuery, CategoryListVm>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<CategoryListVm> Handle(GetAllCategoryQuery request, CancellationToken token)
        {
            IEnumerable<Category> categories;
            try
            {
                categories = await _categoryRepository.GetAllAsync(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (categories.Count() == 0)
            {
                throw new NotFoundException(nameof(Category));
            }

            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return new CategoryListVm { Categories = categoryDtos.ToList() };
        }
    }
}
