using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CozyCare.SharedKernel.Utils
{
	public class FlexibleDateTimeJsonConverter : JsonConverter<DateTime>
	{
		private readonly string[] _formats =
		{
			"yyyy-MM-dd",           // 2024-12-25
            "yyyy-MM-ddTHH:mm:ss",  // 2024-12-25T14:30:45
            "yyyy-MM-dd HH:mm:ss",  // 2024-12-25 14:30:45
            "dd/MM/yyyy",           // 25/12/2024
            "MM/dd/yyyy"            // 12/25/2024
        };

		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var dateString = reader.GetString();

			// Thử parse với từng format
			foreach (var format in _formats)
			{
				if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
				{
					return date;
				}
			}

			// Fallback với parse tự động
			if (DateTime.TryParse(dateString, out var fallbackDate))
			{
				return fallbackDate;
			}

			throw new JsonException($"Unable to convert \"{dateString}\" to DateTime.");
		}

		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		{
			// Luôn output theo format yyyy-MM-dd (chỉ ngày)
			writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
		}
	}
}
