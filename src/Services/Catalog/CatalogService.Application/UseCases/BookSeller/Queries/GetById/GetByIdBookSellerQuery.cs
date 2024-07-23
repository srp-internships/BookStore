using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class GetByIdBookSellerQuery : IRequest<BookSellerDto>
    {
        public Guid Id { get; set; }
    }
}
