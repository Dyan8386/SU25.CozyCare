using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.BookingService.Applications.Externals
{
	public interface IJobApiClient
	{
		Task<BaseResponse<IEnumerable<TaskClaimDto>>> GetTaskByBookingDetailsId(int id, CancellationToken ct = default);
	}
}
