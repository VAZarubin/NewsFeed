using System.Collections.Generic;
using System.Runtime.Caching;
using Configurations;
using DataSource.Cache;
using DB.Facade;
using DB.Facade.User;
using Entites;
using SimpleInjector;

namespace DataSource.Users
{
    public class UserServiceCreator
    {
        #region Static Methods

        public static IUserService CreateUserService(Container parentContainer, Container configurationContainer)
        {
            var container = new Container();

            IUserSource userSource = SourceProvider.GetUserSource(parentContainer);

            var config = configurationContainer.GetInstance<INewsFeedConfiguration>();

            ICacheService<string, IEnumerable<User>> cache =
                CacheServiceBuilder<string, IEnumerable<User>>.CreateService(config.UserInvalidationTime, CacheItemPriority.NotRemovable);

            container.RegisterInstance(userSource);

            container.Register<IUserService, UserService>();

            container.RegisterInstance(cache);

            container.Verify();

            return container.GetInstance<IUserService>();
        }

        #endregion
    }
}
