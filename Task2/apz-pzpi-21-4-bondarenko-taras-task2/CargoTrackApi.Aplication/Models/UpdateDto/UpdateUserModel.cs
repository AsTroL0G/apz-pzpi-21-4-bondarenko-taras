using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Models.Identity;

namespace CargoTrackApi.Application.Models;

public class UpdateUserModel
{
    public TokensModel Tokens { get; set; }

    public UserDto User { get; set; }
}
