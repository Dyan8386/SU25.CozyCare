using Microsoft.AspNetCore.Mvc;

namespace CozyCare.SharedKernel.Base
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult FromBaseResponse<T>(BaseResponse<T> response)
        {
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
