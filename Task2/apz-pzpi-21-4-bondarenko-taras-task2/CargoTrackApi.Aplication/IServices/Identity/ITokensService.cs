using System.Security.Claims;

namespace CargoTrackApi.Application.IServices.Identity;

public interface ITokensService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    string GenerateRefreshToken();

}
