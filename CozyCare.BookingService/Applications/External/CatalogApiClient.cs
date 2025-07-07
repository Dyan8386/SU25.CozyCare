namespace CozyCare.BookingService.Applications.External
{
    public class CatalogApiClient : ICatalogApiClient
    {
        private readonly HttpClient _httpClient;

        public CatalogApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CatalogService");
        }

        public async Task<string> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("/api/categories");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetServiceByIdAsync(Guid serviceId)
        {
            var response = await _httpClient.GetAsync($"/api/services/{serviceId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
