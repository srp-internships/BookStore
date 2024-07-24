namespace OrderService.Domain.Entities;

public record Payment
{
    public string? CardName { get; set; }
    public string? CardNumber { get; set; } 
    public string? Expiration { get; set; } 
    public string? Cvv { get; set; } 
}
