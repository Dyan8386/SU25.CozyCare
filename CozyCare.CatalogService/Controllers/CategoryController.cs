using CozyCare.CatalogService.Application.Interfaces;
using CozyCare.ViewModels.DTOs;
using CozyCare.SharedKernel.Base;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace CozyCare.CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            FromBaseResponse(await _categoryService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            FromBaseResponse(await _categoryService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto) =>
            FromBaseResponse(await _categoryService.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto) =>
            FromBaseResponse(await _categoryService.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            FromBaseResponse(await _categoryService.DeleteAsync(id));

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            Expression<Func<Domain.Entities.Category, bool>> filter = c =>
                string.IsNullOrEmpty(keyword) || c.categoryName.Contains(keyword);
            return FromBaseResponse(await _categoryService.SearchAsync(filter));
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> SetStatus(int id, [FromBody] LockCategoryDto dto) =>
            FromBaseResponse(await _categoryService.SetCategoryStatusAsync(id, dto));
    }
}
