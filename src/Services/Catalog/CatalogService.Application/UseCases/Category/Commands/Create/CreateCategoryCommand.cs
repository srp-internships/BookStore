﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
