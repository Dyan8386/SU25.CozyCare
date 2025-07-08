using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.PaymentService.Application.Externals
{
    public interface IBookingApiClient
    {
        Task<BaseResponse<BookingDto>> GetBookingByIdAsync(int bookingId, CancellationToken ct = default);
    }
}
