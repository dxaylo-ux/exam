using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/categories")]
public class CategoriesController(ICategoryService categoryService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => ToResponse(await categoryService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => ToResponse(await categoryService.GetByIdAsync(id));

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
        => ToResponse(await categoryService.CreateAsync(dto));

    [HttpPut("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        => ToResponse(await categoryService.UpdateAsync(id, dto));

    [HttpDelete("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Delete(int id)
        => ToResponse(await categoryService.DeleteAsync(id));
}
