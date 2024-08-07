using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.IntegrationEvents.SellerEvents
{
    public class SellerUpdatedIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccuredOnUtc { get; set; }
        public Guid SellerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
