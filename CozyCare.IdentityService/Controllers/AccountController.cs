using CozyCare.IdentityService.Application.Interfaces;
using CozyCare.ViewModels.DTOs;
using CozyCare.SharedKernel.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CozyCare.IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Yêu cầu xác thực cho toàn bộ controller
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountService.GetAllAsync();
            return FromBaseResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _accountService.GetByIdAsync(id);
            return FromBaseResponse(result);
        }

        [HttpPost]
        [AllowAnonymous] // Cho phép tạo tài khoản mà không cần đăng nhập
        public async Task<IActionResult> Create([FromBody] CreateAccountRequestDto request)
        {
            var result = await _accountService.CreateAsync(request);
            return FromBaseResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountRequestDto request)
        {
            var result = await _accountService.UpdateAsync(id, request);
            return FromBaseResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _accountService.DeleteAsync(id);
            return FromBaseResponse(result);
        }
    }
}
