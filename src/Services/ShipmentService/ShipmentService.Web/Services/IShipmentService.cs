using ShipmentService.Web.Models;

namespace ShipmentService.Web.Services
{
    public interface IShipmentService
    {
        Task<ShipmentDto> GetShipmentByIdAsync(Guid id);
    }
}
