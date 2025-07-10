using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.JobService.Application.Externals
{
    public interface IJobApiClient
    {
        Task<BaseResponse<BookingDetailDto?>> GetBookingDetailByIdAsync(int id, CancellationToken ct = default);
        Task<BaseResponse<AccountDto>> GetAccountByIdAsync(int id, CancellationToken ct = default);
    }
}
