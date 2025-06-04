namespace CozyCare.API.DTOs.ServiceDetails
{
    public class PostServiceDetailResponse
    {
        public int ServiceDetailId { get; set; }
        public int ServiceId { get; set; }
        public string OptionName { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
    }
}
