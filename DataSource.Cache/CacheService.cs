using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace DataSource.Cache
{
    public class CacheService<TKey, TValue> : ICacheService<TKey, TValue> where TValue : class
    {
        #region Static and Readonly Fields

        private readonly TimeSpan cacheInvalidationTimeout;

        private readonly MemoryCache cacheStorage;
        private readonly Lazy<CacheItemPolicy> lazyCacheItemPolicy;
        private readonly CacheItemPriority priority;

        #endregion

        #region Constructors

        public CacheService(TimeSpan cacheInvalidationTimeout, CacheItemPriority priority)
        {
            this.cacheInvalidationTimeout = cacheInvalidationTimeout;
            this.priority = priority;
            cacheStorage = new MemoryCache(nameof(TValue));

            lazyCacheItemPolicy = new Lazy<CacheItemPolicy>(CreateCacheItemPolicy);
        }

        #endregion

        #region Methods

        private CacheItemPolicy CreateCacheItemPolicy()
        {
            return new CacheItemPolicy { SlidingExpiration = cacheInvalidationTimeout, Priority = priority };
        }

        #endregion

        #region ICacheService<TKey,TValue> Members

        public async Task<TValue> GetOrAddAsync(TKey key, Func<Task<TValue>> valueGetter)
        {
            object value = cacheStorage.Get(key.ToString());

            if (value == null)
            {
                value = await valueGetter.Invoke();

                cacheStorage.Add(new CacheItem(key.ToString(), value), lazyCacheItemPolicy.Value);
            }

            return (TValue)value;
        }

        #endregion
    }
}
