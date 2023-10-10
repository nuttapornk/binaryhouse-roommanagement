namespace Application.Common.Caching.Interfaces;

public interface IRedisCacheService
{
    public Task Set<T>(string key, T value, double lifeSpan = 14400, bool sliding = false);
    public Task<T?> Get<T>(string key);
    public void Remove(string key);
}
