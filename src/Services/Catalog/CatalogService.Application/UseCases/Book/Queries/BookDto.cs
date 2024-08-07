using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CatalogService.Application.Mappers;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.UseCases.Queries
{
    public class BookDto : IMapWith<Book>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ISBN { get; set; }
        public PublisherDto Publisher { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }
        public ICollection<AuthorDto> Authors { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));
        }
    }
}
