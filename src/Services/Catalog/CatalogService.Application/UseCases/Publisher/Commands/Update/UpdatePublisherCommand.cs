﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class UpdatePublisherCommand : IRequest
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
    }
}
