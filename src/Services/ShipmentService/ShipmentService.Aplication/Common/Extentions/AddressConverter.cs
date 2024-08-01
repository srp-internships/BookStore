using OrderService.IntegrationEvents;
using ShipmentService.Domain.Entities.Shipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.Common.Extentions
{
    public static class AddressConverter
    {
        public static Address ToAddress(ShippingAddress shippingAddress, string firstName, string lastName, string emailAddress)
        {
            return new Address(
                firstName,
                lastName,
                emailAddress,
                shippingAddress.Country ?? string.Empty,
                shippingAddress.City ?? string.Empty,
                shippingAddress.Street ?? string.Empty
            );
        }

        public static ShippingAddress ToShippingAddress(Address address)
        {
            return new ShippingAddress
            {
                Country = address.Country,
                City = address.State, 
                Street = address.Street
            };
        }
    }
}
