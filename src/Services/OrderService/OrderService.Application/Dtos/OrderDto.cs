﻿using OrderService.Domain.Enums;

namespace OrderService.Application.Dtos;

public record OrderDto(
    Guid CustomerId,
    Guid CartId,
    AddressDto ShippingAddress,
    OrderStatus Status,
    List<OrderItemDto> Items);
