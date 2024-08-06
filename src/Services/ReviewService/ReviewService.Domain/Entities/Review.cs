﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string? Comment { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; } 
        public DateTime CreatedDate { get; set; }
    }
}
