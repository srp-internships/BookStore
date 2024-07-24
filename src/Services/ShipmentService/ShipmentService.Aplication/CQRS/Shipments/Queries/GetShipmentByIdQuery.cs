﻿using MediatR;
using ShipmentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.CQRS.Shipments.Queries
{
    public class GetShipmentByIdQuery : IRequest<Shipment>
    {
        public Guid ShipmentId { get; set; }
    }
}
