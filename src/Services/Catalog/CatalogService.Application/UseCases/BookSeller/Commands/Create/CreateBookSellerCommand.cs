using AutoMapper;
using CatalogService.Application.Mappers;
using CatalogService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreateBookSellerCommand : IRequest, IMapWith<BookSeller>
    {
        public Guid BookId { get; set; }
        public Guid SellerId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBookSellerCommand, BookSeller>();
        }
    }
}
