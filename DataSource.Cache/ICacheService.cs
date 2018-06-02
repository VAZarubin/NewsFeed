using System;
using System.Threading.Tasks;

namespace DataSource.Cache
{
    public interface ICacheService<TKey, TValue> where TValue : class
    {
        #region Methods

        #endregion

        Task<TValue> GetOrAddAsync(TKey key, Func<Task<TValue>> valueGetter);
    }
}
