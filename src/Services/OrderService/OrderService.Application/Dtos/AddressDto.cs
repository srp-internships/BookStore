﻿namespace OrderService.Application.Dtos;

public record AddressDto(string FirstName, string LastName, string EmailAddress, string Country, string State, string ZipCode);
