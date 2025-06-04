

namespace CozyCare.API.DTOs.ServiceDetails
{
    public class GetServiceDetailResponse
    {
        public int ServiceDetailId { get; set; }

        public int? ServiceId { get; set; }

        public string OptionName { get; set; } = string.Empty;

        public string OptionType { get; set; } = string.Empty;

        public decimal? BasePrice { get; set; }

        public string Unit { get; set; } = string.Empty;

        public int? Duration { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool? IsActive { get; set; }

    }
}
