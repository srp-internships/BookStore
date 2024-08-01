﻿using AutoMapper;
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
    public class UpdateBookCommand : IRequest, IMapWith<Book>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBookCommand, Book>();
        }
    }
}
