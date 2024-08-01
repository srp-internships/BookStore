using AutoMapper;
using CatalogService.Application.Mappers;

using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class AuthorListVm : IMapWith<AuthorDto>
    {
        public List<AuthorDto> Authors { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Author, AuthorDto>();
        }
    }
}
