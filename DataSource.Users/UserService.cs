using System.Collections.Generic;
using System.Threading.Tasks;
using DataSource.Cache;
using DB.Facade.User;
using Entites;

namespace DataSource.Users
{
    internal class UserService : IUserService
    {
        #region Constants

        private const string cacheKey = "Users";

        #endregion

        #region Static and Readonly Fields

        private readonly ICacheService<string, IEnumerable<User>> userCacheService;

        private readonly IUserSource userSource;

        #endregion

        #region Constructors

        public UserService(IUserSource userSource, ICacheService<string, IEnumerable<User>> userCacheService)
        {
            this.userSource = userSource;
            this.userCacheService = userCacheService;
        }

        #endregion

        #region IUserService Members

        public Task<IEnumerable<User>> GetUserList()
        {
            return userCacheService.GetOrAddAsync(cacheKey, async () => await Task.Run(() => userSource.GetUsers()));
        }

        #endregion
    }
}
