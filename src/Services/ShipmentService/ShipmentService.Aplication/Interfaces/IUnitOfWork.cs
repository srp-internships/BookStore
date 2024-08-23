using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Aplication.Interfaces
{
    public interface IUnitOfWork
    {
        IShipmentRepository Shipments { get; }
        Task<int> CompleteAsync();
    }
}
