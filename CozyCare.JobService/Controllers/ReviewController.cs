using CozyCare.JobService.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CozyCare.JobService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IJobUnitOfWork _reviewService;
      
    }
}
