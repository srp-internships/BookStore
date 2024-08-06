using AutoMapper;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using CatalogService.Infostructure;
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
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IValidator<UpdateAuthorCommand> _validator = validator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(UpdateAuthorCommand request, CancellationToken token)
        {
            await _validator.ValidateAsync(request, token);

            var author = await _authorRepository.GetByIdAsync(request.Id, token);
            author.Name = request.Name;
            author.Description = request.Description;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
