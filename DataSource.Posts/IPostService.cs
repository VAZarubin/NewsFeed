using System.Collections.Generic;
using System.Threading.Tasks;
using Entites;
using Entites.Contracts;

namespace DataSource.Posts
{
    public interface IPostService
    {
        #region Methods

        Task<Post> GetPostById(PostId postId);

        Task<IEnumerable<PostSummary>> GetPostSummaries();

        #endregion
    }
}
