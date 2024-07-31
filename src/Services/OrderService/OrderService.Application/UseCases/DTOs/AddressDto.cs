namespace OrderService.Application.UseCases.DTOs;

public record AddressDto(string FirstName, string LastName, string EmailAddress, string Country, string State, string Street);
