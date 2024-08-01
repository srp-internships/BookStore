using AutoMapper;
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
    public class GetByIdAuthorQueryHandler(
        IAuthorRepository authorRepository,
        IMapper mapper) : IRequestHandler<GetByIdAuthorQuery, AuthorDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<AuthorDto> Handle(GetByIdAuthorQuery request, CancellationToken token)
        {
            Author author;

            try
            {
                author = await _authorRepository.GetByIdAsync(request.Id, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (author == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            } 

            return _mapper.Map<AuthorDto>(author);
        }
    }
}
