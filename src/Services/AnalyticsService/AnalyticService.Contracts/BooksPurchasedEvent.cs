using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticService.Contracts
{
    public class BooksPurchasedEvent
    {
        public List<Book> Books { get; set; }
    }
    
}
