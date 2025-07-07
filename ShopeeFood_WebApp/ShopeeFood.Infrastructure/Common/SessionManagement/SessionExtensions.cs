using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopeeFood.Infrastructure.Common.SessionManagement
{
    public static class SessionExtensions
    {
        static JsonSerializerOptions _JsonSerializerOptions => new()
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        public static T? GetObjectFromByte<T>(byte[] byteArray)
        {
            if (byteArray is not null && byteArray.Any())
            {
                return System.Text.Json.JsonSerializer.Deserialize<T>(byteArray, _JsonSerializerOptions);
            }

            return default;
        }

        public static byte[] GetByteFromObject<T>(T sessionObject)
        {
            if (sessionObject is null)
            {
                return Array.Empty<byte>();
            }

            return Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(sessionObject, _JsonSerializerOptions));
        }

    }
}
