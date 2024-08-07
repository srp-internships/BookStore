using CatalogService.Domain.Interfaces;
using CatalogService.Infostructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class DeleteCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(DeleteCategoryCommand request, CancellationToken token)
        {
            await _categoryRepository.DeleteAsync(request.Id, token);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
