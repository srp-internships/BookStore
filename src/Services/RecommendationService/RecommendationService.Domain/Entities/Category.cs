using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationService.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public List<Book> Books { get; set; }
    }
}
