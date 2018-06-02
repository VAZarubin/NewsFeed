using System.Collections.Generic;
using System.Threading.Tasks;
using Entites;
using Entites.Contracts;

namespace DataSource.Comments
{
    public interface ICommentService
    {
        #region Methods

        Task<IEnumerable<Comment>> GetPostCommentList(PostId postId);

        #endregion
    }
}
