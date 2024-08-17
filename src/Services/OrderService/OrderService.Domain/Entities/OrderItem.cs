﻿namespace OrderService.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Order? Order { get; set; }
    public Guid OrderId { get; set; }
    public Guid BookId { get; set; }
    public Guid SellerId { get; set; }
    public string? Title { get; set; }

}
