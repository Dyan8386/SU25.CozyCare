namespace CozyCare.BookingService.Applications.External
{
    public interface ICatalogApiClient
    {
        Task<string> GetAllCategoriesAsync();
        Task<string> GetServiceByIdAsync(Guid serviceId);
    }
}
