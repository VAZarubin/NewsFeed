using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Client.ServicesClient.Services.Comments
{
    public interface ICommentService
    {
        #region Methods

        Task<IEnumerable<Comment>> GetCommentForPost(int id);

        #endregion
    }
}
