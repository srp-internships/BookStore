﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticService.Domain.Entities
{
    public class Book
    {
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
    }
}
