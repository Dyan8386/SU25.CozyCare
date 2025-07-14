using CozyCare.IdentityService.Application.Interfaces;
using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CozyCare.IdentityService.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.AuthenticateAsync(request);
            return FromBaseResponse(result);
        }

        [HttpGet("verify")]
        [Authorize]
        public IActionResult VerifyToken()
        {
            return Ok(BaseResponse<string>.OkResponse("Token is valid"));
        }

    }
}
