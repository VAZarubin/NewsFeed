using System;
using System.Runtime.Caching;

namespace DataSource.Cache
{
    public class CacheServiceBuilder<TKey, TValue> where TValue : class
    {
        #region Static Methods

        public static ICacheService<TKey, TValue> CreateService(TimeSpan cacheInvalidationTimeout, CacheItemPriority priority)
        {
            return new CacheService<TKey, TValue>(cacheInvalidationTimeout, priority);
        }

        #endregion
    }
}
