using CargoTrackApi.Domain.Entities;

namespace CargoTrackApi.Application.Models.Dtos;

public class UserDto
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Patronym { get; set; }

    public string? PhoneNumber { get; set; }

    public Guid? GuestId { get; set; }

    public List<RoleDto> Roles { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
