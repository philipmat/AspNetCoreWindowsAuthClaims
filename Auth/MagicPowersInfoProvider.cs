using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreWindowsAuthClaims.Auth
{
    public class MagicPowersInfoProvider
    {
        private const string CacheClaimsKey = "CacheClaims";
        private const int ClaimCacheInSeconds = 5 * 60;
        private readonly bool _cacheClaims;
        private readonly IMemoryCache _memoryCache;

        public MagicPowersInfoProvider(IConfiguration config, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheClaims = config.GetValue<bool>(CacheClaimsKey);
        }

        public async Task<bool> CanHasPowerAsync(string userId)
        {
            if (!_cacheClaims)
            {
                return await ExpensiveHasPowerOperation(userId);
            }

            return await _memoryCache.GetOrCreateAsync<bool>(
                $"power-{userId}",
                async cacheEntry =>
                {
                    // or use cacheEntry.AbsoluteExpiration
                    cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(ClaimCacheInSeconds);
                    bool hasPower = await ExpensiveHasPowerOperation(userId);
                    return hasPower;
                });
        }

        private Task<bool> ExpensiveHasPowerOperation(string userId)
            => Task.FromResult(true);
    }
}
