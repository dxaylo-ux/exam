using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UsersController(IUserService userService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] UserFilter filter, CancellationToken ct)
        => ToResponse(await userService.GetPagedAsync(filter, ct));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => id == CurrentUserId || IsAdmin
            ? ToResponse(await userService.GetByIdAsync(id))
            : Forbid();

    [HttpPut("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        => id == CurrentUserId || IsAdmin
            ? ToResponse(await userService.UpdateAsync(id, dto))
            : Forbid();

    [HttpDelete("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Delete(int id)
        => ToResponse(await userService.DeleteAsync(id));
}