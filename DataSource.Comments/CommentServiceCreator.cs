using System.Collections.Generic;
using System.Runtime.Caching;
using Configurations;
using DataSource.Cache;
using DB.Facade;
using Entites;
using Entites.Contracts;
using SimpleInjector;

namespace DataSource.Comments
{
    public class CommentServiceCreator
    {
        #region Static Methods

        public static ICommentService CreateCommentService(Container parentContainer, Container configurationContainer)
        {
            var container = new Container();

            container.RegisterInstance(SourceProvider.CommentSource(parentContainer));

            var config = configurationContainer.GetInstance<INewsFeedConfiguration>();

            ICacheService<PostId, IEnumerable<Comment>> cache =
                CacheServiceBuilder<PostId, IEnumerable<Comment>>.CreateService(config.CommentInvalidationTime, CacheItemPriority.Default);

            container.Register<ICommentService, CommentService>();

            container.RegisterInstance(cache);

            container.Verify();

            return container.GetInstance<ICommentService>();
        }

        #endregion
    }
}
