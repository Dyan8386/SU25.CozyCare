using CozyCare.CatalogService.Application.Interfaces;
using CozyCare.ViewModels.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CozyCare.CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDto dto) =>
            Ok(await _service.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceDto dto) =>
            Ok(await _service.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            Ok(await _service.DeleteAsync(id));

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            Expression<Func<Domain.Entities.Service, bool>> filter = s =>
                string.IsNullOrEmpty(keyword) || s.serviceName.Contains(keyword);
            return Ok(await _service.SearchAsync(filter));
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> SetStatus(int id, [FromBody] LockServiceDto dto) =>
            Ok(await _service.SetServiceStatusAsync(id, dto));

    }
}
