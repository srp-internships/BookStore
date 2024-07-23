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
    public class GetByIdAuthorQueryHandler(
        IAuthorRepository authorRepository,
        IMapper mapper) : IRequestHandler<GetByIdAuthorQuery, AuthorDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<AuthorDto> Handle(GetByIdAuthorQuery query, CancellationToken token)
        {
            Author author = await _authorRepository.GetByIdAsync(query.Id, token);

            return _mapper.Map<AuthorDto>(author);
        }
    }
}
