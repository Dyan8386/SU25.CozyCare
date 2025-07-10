using CozyCare.JobService.Application.Interfaces;
using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CozyCare.JobService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskClaimStatusController : BaseApiController
    {
        private readonly ITaskClaimStatusService _taskClaimStatusService;

        public TaskClaimStatusController(ITaskClaimStatusService taskClaimStatusService)
        {
            _taskClaimStatusService = taskClaimStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            FromBaseResponse(await _taskClaimStatusService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            FromBaseResponse(await _taskClaimStatusService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAndUpdateTaskClaimStatusDto dto) =>
            FromBaseResponse(await _taskClaimStatusService.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateAndUpdateTaskClaimStatusDto dto) =>
            FromBaseResponse(await _taskClaimStatusService.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            FromBaseResponse(await _taskClaimStatusService.DeleteAsync(id));

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            Expression<Func<Domain.Entities.TaskClaimStatus, bool>> filter = c =>
                string.IsNullOrEmpty(keyword) || c.statusName.Contains(keyword);
            return FromBaseResponse(await _taskClaimStatusService.SearchAsync(filter));
        }
    }
}
