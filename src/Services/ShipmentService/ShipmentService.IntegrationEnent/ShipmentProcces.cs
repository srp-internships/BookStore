namespace ShipmentService.IntegrationEnent
{
    public class ShipmentProcces
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime estimatedDeliveryDate {  get; set; }
        public string Message { get; set; }
    }
}
