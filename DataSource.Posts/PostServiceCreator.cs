using System.Collections.Generic;
using System.Runtime.Caching;
using Configurations;
using DataSource.Cache;
using DB.Facade;
using Entites;
using Entites.Contracts;
using SimpleInjector;

namespace DataSource.Posts
{
    public class PostServiceCreator
    {
        #region Static Methods

        public static IPostService CreatePostService(Container parentContainer, Container configurationContainer)
        {
            var container = new Container();

            var config = configurationContainer.GetInstance<INewsFeedConfiguration>();

            ICacheService<PostId, Post> postCache = CacheServiceBuilder<PostId, Post>.CreateService(config.PostInvalidationTime,
                CacheItemPriority.NotRemovable);

            ICacheService<string, IEnumerable<PostSummary>> postSummaryCache =
                CacheServiceBuilder<string, IEnumerable<PostSummary>>.CreateService(config.SummariesInvalidationTime,
                    CacheItemPriority.Default);

            container.RegisterInstance(SourceProvider.GetPostSource(parentContainer));

            container.Register<IPostService, PostServiceCache>();

            container.RegisterInstance(postCache);

            container.RegisterInstance(postSummaryCache);

            container.Verify();

            return container.GetInstance<IPostService>();
        }

        #endregion
    }
}
