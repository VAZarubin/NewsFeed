using System.Collections.Generic;
using System.Threading.Tasks;
using DataSource.Cache;
using DB.Facade.Post;
using Entites;
using Entites.Contracts;

namespace DataSource.Posts
{
    internal class PostServiceCache : IPostService
    {
        #region Constants

        private const string cacheKey = "PostSummaries";

        #endregion

        #region Static and Readonly Fields

        private readonly ICacheService<PostId, Post> postCacheService;

        private readonly IPostSource postRepository;
        private readonly ICacheService<string, IEnumerable<PostSummary>> postSummariesCacheService;

        #endregion

        #region Constructors

        public PostServiceCache(IPostSource postRepository,
            ICacheService<PostId, Post> postCacheService,
            ICacheService<string, IEnumerable<PostSummary>> postSummariesCacheService)
        {
            this.postRepository = postRepository;
            this.postCacheService = postCacheService;
            this.postSummariesCacheService = postSummariesCacheService;
        }

        #endregion

        #region IPostService Members

        public async Task<Post> GetPostById(PostId postId)
        {
            return await postCacheService.GetOrAddAsync(postId, async () => await Task.Run(() => postRepository.GetPost(postId)));
        }

        public async Task<IEnumerable<PostSummary>> GetPostSummaries()
        {
            return
                await
                    postSummariesCacheService.GetOrAddAsync(cacheKey, async () => await Task.Run(() => postRepository.GetPostSummaries()));
        }

        #endregion
    }
}
