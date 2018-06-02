using System.Collections.Generic;
using System.Threading.Tasks;
using DataSource.Cache;
using DB.Facade.Comment;
using Entites;
using Entites.Contracts;

namespace DataSource.Comments
{
    internal class CommentService : ICommentService
    {
        #region Static and Readonly Fields

        private readonly ICommentSource commentRepository;
        private readonly ICacheService<PostId, IEnumerable<Comment>> commentServiceCache;

        #endregion

        #region Constructors

        public CommentService(ICommentSource commentRepository, ICacheService<PostId, IEnumerable<Comment>> commentServiceCache)
        {
            this.commentRepository = commentRepository;
            this.commentServiceCache = commentServiceCache;
        }

        #endregion

        #region ICommentService Members

        public async Task<IEnumerable<Comment>> GetPostCommentList(PostId postId)
        {
            return
                await commentServiceCache.GetOrAddAsync(postId, async () => await Task.Run(() => commentRepository.GetComments(postId)));
        }

        #endregion
    }
}
