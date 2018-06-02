using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public interface ICommentRepository
    {
        #region Methods

        Task<IEnumerable<Comment>> GetCommentForPostAsync(int postId);

        #endregion
    }
}
