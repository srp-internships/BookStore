using AutoMapper;

using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var authors = _authorRepository.GetAllAsync(token);
            var authorsVm = _mapper.Map<AuthorListVm>(authors);
            return authorsVm;
        }
    }
}
