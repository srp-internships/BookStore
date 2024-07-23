using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Application.UseCases;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookCommand, Book>()
                .ForMember(dest => dest.BookCategories, opt => opt.Ignore())
                .ForMember(dest => dest.BookAuthors, opt => opt.Ignore())
                .ForMember(dest => dest.BookSellers, opt => opt.Ignore())
                .ForMember(dest => dest.Publisher, opt => opt.Ignore())
                .ForMember(dest => dest.Authors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Sellers, opt => opt.Ignore());

            CreateMap<CreateAuthorCommand, Author>();
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<CreatePublisherCommand, Publisher>();

            CreateMap<Book, BookDto>();

            CreateMap<Author, AuthorDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Publisher, PublisherDto>();
            CreateMap<IEnumerable<Author>, AuthorListVm>();
            CreateMap<IEnumerable<Category>, CategoryListVm>();
        }
    }
}
