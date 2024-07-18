using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticService.Domain.Entities
{
    public class AnalyticsReport
    {
        public DateTime Date { get; set; }
        public int TotalBooksSold { get; set; }
    }
}
