﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; } 
        public Guid BookId { get; set; } 
        public Guid UserId { get; set; } 
        public string? Comment { get; set; }
        public int Rating { get; set; } 
        public DateTime CreatedDate { get; set; } 
    }
}
