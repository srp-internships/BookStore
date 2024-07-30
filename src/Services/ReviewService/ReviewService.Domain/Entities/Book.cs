using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReviewService.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; }= new List<Review>();
    }
}

