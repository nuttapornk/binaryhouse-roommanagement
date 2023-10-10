using Application.Common.Caching.Interfaces;
using Hyperion;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Caching;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _cache;
    private readonly Serializer _serializer;

    [Obsolete]
    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
        _serializer = new Serializer(new SerializerOptions(preserveObjectReferences: true));
    }

    public async Task Set<T>(string key, T value, double lifeSpan, bool sliding)
    {
        await using var memory = new MemoryStream();
        _serializer.Serialize(value, memory);

        var bytes = memory.ToArray();
        DistributedCacheEntryOptions options = new();

        if (sliding)
        {
            options.SlidingExpiration = TimeSpan.FromSeconds(lifeSpan);
        }
        else
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(lifeSpan);
        }
        await _cache.SetAsync(key, bytes, options);
    }

    public async Task<T?> Get<T>(string key)
    {
        var bytes = await _cache.GetAsync(key);
        if (bytes == null)
        {
            return default;
        }
        await using var memory = new MemoryStream(bytes);
        return _serializer.Deserialize<T>(memory);
    }

    public async void Remove(string key)
    {
        await _cache.RemoveAsync(key);
    }


}
