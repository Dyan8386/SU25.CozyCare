using CozyCare.CatalogService.Application.Interfaces;
using CozyCare.ViewModels.DTOs;
using CozyCare.SharedKernel.Base;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CozyCare.CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : BaseApiController
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            FromBaseResponse(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            FromBaseResponse(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDto dto) =>
            FromBaseResponse(await _service.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceDto dto) =>
            FromBaseResponse(await _service.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            FromBaseResponse(await _service.DeleteAsync(id));

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            Expression<Func<Domain.Entities.Service, bool>> filter = s =>
                string.IsNullOrEmpty(keyword) || s.serviceName.Contains(keyword);
            return FromBaseResponse(await _service.SearchAsync(filter));
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> SetStatus(int id, [FromBody] LockServiceDto dto) =>
            FromBaseResponse(await _service.SetServiceStatusAsync(id, dto));
    }
}
