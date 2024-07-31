using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Domain.DTOs
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
