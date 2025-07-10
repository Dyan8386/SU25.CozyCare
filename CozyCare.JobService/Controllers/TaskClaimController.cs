using CozyCare.JobService.Application.Interfaces;
using CozyCare.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using CozyCare.SharedKernel.Base;
namespace CozyCare.JobService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskClaimController : BaseApiController
    {
        private readonly ITaskClaimService _taskClaimService;

        public TaskClaimController(ITaskClaimService taskClaimService)
        {
            _taskClaimService = taskClaimService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            FromBaseResponse(await _taskClaimService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            FromBaseResponse(await _taskClaimService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskClaimDto dto) =>
            FromBaseResponse(await _taskClaimService.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskClaimDto dto) =>
            FromBaseResponse(await _taskClaimService.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            FromBaseResponse(await _taskClaimService.DeleteAsync(id));

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            Expression<Func<Domain.Entities.TaskClaim, bool>> filter = c =>
                string.IsNullOrEmpty(keyword) || c.note.Contains(keyword);
            return FromBaseResponse(await _taskClaimService.SearchAsync(filter));
        }
    }
}
