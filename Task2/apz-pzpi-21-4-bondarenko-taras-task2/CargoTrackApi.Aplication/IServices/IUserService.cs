using CargoTrackApi.Application.Models.CreateDto;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Models.Identity;
using CargoTrackApi.Application.Models.UpdateDto;
using CargoTrackApi.Application.Models;
using CargoTrackApi.Application.Paging;


namespace CargoTrackApi.Application.IServices
{
    public interface IUserService
    {
        Task<UserDto> AddUserAsync(UserCreateDto dto, CancellationToken cancellationToken);

        Task<UserDto> GetUserAsync(string id, CancellationToken cancellationToken);

        Task<PagedList<UserDto>> GetUsersPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<UpdateUserModel> UpdateAsync(UserUpdateDto userDto, CancellationToken cancellationToken);

        Task<TokensModel> LoginAsync(LoginUserDto login, CancellationToken cancellationToken);

        Task<UserDto> AddToRoleAsync(string userId, string roleName, CancellationToken cancellationToken);

        Task<UserDto> RemoveFromRoleAsync(string userId, string roleName, CancellationToken cancellationToken);

        Task<UserDto> DeleteUser(string valueId, CancellationToken cancellationToken);
    }
}
