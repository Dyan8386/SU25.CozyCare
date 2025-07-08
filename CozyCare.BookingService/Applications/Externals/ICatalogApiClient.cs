using CozyCare.SharedKernel.Base;
using CozyCare.ViewModels.DTOs;

namespace CozyCare.BookingService.Applications.Externals
{
    public interface ICatalogApiClient
    {
        Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(CancellationToken ct = default);
        Task<BaseResponse <ServiceDto>>GetServiceByIdAsync(int serviceId, CancellationToken ct = default);
    }
}
