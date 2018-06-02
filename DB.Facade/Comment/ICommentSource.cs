using System.Collections.Generic;
using Entites.Contracts;

namespace DB.Facade.Comment
{
    public interface ICommentSource
    {
        #region Methods

        IEnumerable<Entites.Comment> GetComments(PostId postId);

        #endregion
    }
}
