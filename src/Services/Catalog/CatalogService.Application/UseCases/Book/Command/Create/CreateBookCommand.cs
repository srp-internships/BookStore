using AutoMapper;
using CatalogService.Application.Mappers;
using CatalogService.Application.Models;
using CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreateBookCommand : IRequest<Guid>, IMapWith<Book>
    {
        public string Title { get; set; }
        public FileRequest Image { get; set; }
        public string ISBN { get; set; }
        public Guid PublisherId { get; set; }
        public ICollection<Guid> CategoryIds {  get; set; }
        public ICollection<Guid> AuthorIds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBookCommand, Book>()
                .ForMember(dest => dest.Authors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.BookSellers, opt => opt.Ignore())
                .ForMember(dest => dest.Publisher, opt => opt.Ignore())
                .ForMember(dest => dest.Authors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Sellers, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
