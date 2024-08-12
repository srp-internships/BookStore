using AutoMapper;
using CatalogService.Domain.Entities;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repositories;
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
            Category category;

            try
            {
                category = await _categoryRepository.GetByIdAsync(request.Id, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            } 

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
