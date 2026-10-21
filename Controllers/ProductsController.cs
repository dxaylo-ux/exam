using Application.DTOs;
using Application.Interfaces;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/products")]
public class ProductsController(IProductService productService) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductFilter filter, CancellationToken ct)
        => ToResponse(await productService.GetPagedAsync(filter, ct));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => ToResponse(await productService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
        => ToResponse(await productService.CreateAsync(dto, CurrentUserId));

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        => ToResponse(await productService.UpdateAsync(id, dto, CurrentUserId, IsAdmin));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => ToResponse(await productService.DeleteAsync(id, CurrentUserId, IsAdmin));
}
