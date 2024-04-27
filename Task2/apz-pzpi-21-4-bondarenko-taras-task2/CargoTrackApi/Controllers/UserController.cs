using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Models;
using CargoTrackApi.Application.Models.CreateDto;
using CargoTrackApi.Application.Models.UpdateDto;
using CargoTrackApi.Application.Models.Identity;
using CargoTrackApi.Application.Paging;
using CargoTrackApi.Infrastructure.Services;
using CargoTrackApi.Domain.Entities;

namespace CargoTrackApi.Api.Controllers;

[Route("users")]
public class UserController : BaseController
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync([FromBody] UserCreateDto register, CancellationToken cancellationToken)
    {
        await _userService.AddUserAsync(register, cancellationToken);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokensModel>> LoginAsync([FromBody] LoginUserDto login, CancellationToken cancellationToken)
    {
        var tokens = await _userService.LoginAsync(login, cancellationToken);
        return Ok(tokens);
    }

   // [Authorize]
    [HttpPut]
    public async Task<ActionResult<UpdateUserModel>> UpdateAsync([FromBody] UserUpdateDto userDto, CancellationToken cancellationToken)
    {
        var updatedUserModel = await _userService.UpdateAsync(userDto, cancellationToken);
        return Ok(updatedUserModel);
    }

   // [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync(id, cancellationToken);
        return Ok(user);
    }

   // [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<PagedList<UserDto>>> GetUsersPageAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var users = await _userService.GetUsersPageAsync(pageNumber, pageSize, cancellationToken);
        return Ok(users);
    }

   // [Authorize(Roles = "Admin")]
    [HttpPost("{userId}/roles/{roleName}")]
    public async Task<ActionResult<PagedList<UserDto>>> AddToRoleAsync(string userId, string roleName, CancellationToken cancellationToken)
    {
        var users = await _userService.AddToRoleAsync(userId, roleName, cancellationToken);
        return Ok(users);
    }

   // [Authorize(Roles = "Admin")]
    [HttpDelete("{userId}/roles/{roleName}")]
    public async Task<ActionResult<PagedList<UserDto>>> RemoveFromeRoleAsync(string userId, string roleName, CancellationToken cancellationToken)
    {
        var users = await _userService.RemoveFromRoleAsync(userId, roleName, cancellationToken);
        return Ok(users);
    }

    [HttpDelete("delete/{userid}")]
    public async Task<ActionResult<UserDto>> DeleteUserSettings(string userid, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteUser(userid, cancellationToken);
        return Ok(result);
    }
}