using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Infrastructure.Caching
{
    internal static class DistributedCacheExtensions
    {
        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
        }

        public static Task SetAsync<T>(
            this IDistributedCache cache,
            string key,
            T value,
            DistributedCacheEntryOptions options)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, value);
                bytes = memoryStream.ToArray();
            }

            return cache.SetAsync(key, bytes, options);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var val = await cache.GetAsync(key);
            var result = default(T);

            if (val == null) return result;

            using (var memoryStream = new MemoryStream(val))
            {
                var binaryFormatter = new BinaryFormatter();
                result = (T)binaryFormatter.Deserialize(memoryStream);
            }

            return result;
        }

        public static async Task<(bool Found, T Value)> TryGetAsync<T>(this IDistributedCache cache, string key)
        {
            var cachedValue = await cache.GetAsync(key);
            T value;

            if (cachedValue == null)
            {
                return (false, default(T));
            }

            using (var memoryStream = new MemoryStream(cachedValue))
            {
                var binaryFormatter = new BinaryFormatter();
                value = (T)binaryFormatter.Deserialize(memoryStream);
            }

            return (true, value);
        }
    }
}
