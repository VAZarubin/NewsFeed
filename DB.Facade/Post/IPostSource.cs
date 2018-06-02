using System.Collections.Generic;
using Entites;
using Entites.Contracts;

namespace DB.Facade.Post
{
    public interface IPostSource
    {
        #region Methods

        Entites.Post GetPost(PostId postId);

        IEnumerable<PostSummary> GetPostSummaries();

        #endregion
    }
}
