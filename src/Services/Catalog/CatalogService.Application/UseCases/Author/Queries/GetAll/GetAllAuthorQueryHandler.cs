using AutoMapper;
using CatalogService.Application.Exceptions;

using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetAllAuthorQueryHandler(
        IAuthorRepository authorRepository,
        IMapper mapper) : IRequestHandler<GetAllAuthorQuery, AuthorListVm>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<AuthorListVm> Handle(GetAllAuthorQuery request, CancellationToken token)
        {
            IEnumerable<Author> authors;
            try
            {
                authors = await _authorRepository.GetAllAsync(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }

            if (authors.Count() == 0)
            {
                throw new NotFoundException(nameof(Author));
            }
            
            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return new AuthorListVm { Authors = authorDtos.ToList() };
        }
    }
}
