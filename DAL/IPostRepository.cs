using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public interface IPostRepository
    {
        #region Methods

        Task<Post> GetPostById(int id);

        Task<IEnumerable<PostSummary>> GetPostList();

        #endregion
    }
}
