using log4net.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopeeFood.Infrastructure.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace ShopeeFood.Infrastructure.Common.Cache
{
    public static class CacheKey
    {
        public const string CustomerProfileKey = "CustomerProfileKey";
        public const string CustomerAddressesKey = "CustomerAddressesKey";
        public const string AccessToken = "AccessToken";
    }

    public interface ICacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default);
        Task<T?> GetAsync<T>(string key, CancellationToken ct = default);
        Task RemoveAsync(string key, CancellationToken ct = default);
        string CreateCacheKey(HttpContext httpContext, string dataKey);
        string CreateCacheKey(string dataKey);
    }

    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private readonly IConfiguration _configuration;

        public RedisCacheService(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
        {
            var json = JsonConvert.SerializeObject(value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl ?? TimeSpan.FromMinutes(30)
            };
            await _cache.SetStringAsync(key, json, options, ct);
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
        {
            var json = await _cache.GetStringAsync(key, ct);
            var result = JsonConvert.DeserializeObject<T>(json);

            return result ?? default;
        }

        public Task RemoveAsync(string key, CancellationToken ct = default) => _cache.RemoveAsync(key, ct);

        public string CreateCacheKey(HttpContext httpContext, string dataKey)
        {
            var redisName = _configuration["Redis:InstanceName"];
            var domain = httpContext.Request.Host.Host;
            return $"{redisName.ToLower()}_{domain}_{dataKey}".ToLower();
        }

        public string CreateCacheKey(string dataKey)
        {
            var redisName = _configuration["Redis:InstanceName"];
            return $"{redisName.ToLower()}_{dataKey}".ToLower();
        }
    }
}
