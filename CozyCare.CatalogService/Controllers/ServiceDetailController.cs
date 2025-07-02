using CozyCare.CatalogService.Application.Interfaces;
using CozyCare.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CozyCare.CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceDetailController : ControllerBase
    {
        private readonly IServiceDetailService _serviceDetail;

        public ServiceDetailController(IServiceDetailService serviceDetail)
        {
            _serviceDetail = serviceDetail;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _serviceDetail.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await _serviceDetail.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDetailDto dto) =>
            Ok(await _serviceDetail.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceDetailDto dto) =>
            Ok(await _serviceDetail.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            Ok(await _serviceDetail.DeleteAsync(id));

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            Expression<Func<Domain.Entities.ServiceDetail, bool>> filter = d =>
                string.IsNullOrEmpty(keyword) || d.description.Contains(keyword);
            return Ok(await _serviceDetail.SearchAsync(filter));
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> SetStatus(int id, [FromBody] LockServiceDetailDto dto) =>
            Ok(await _serviceDetail.SetServiceDetailStatusAsync(id, dto));

    }
}
