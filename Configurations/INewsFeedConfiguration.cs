using System;

namespace Configurations
{
    public interface INewsFeedConfiguration
    {
        #region Properties

        TimeSpan CommentInvalidationTime { get; }

        string DatabaseConnection { get; }
        TimeSpan PostInvalidationTime { get; }
        TimeSpan UserInvalidationTime { get; }
        TimeSpan SummariesInvalidationTime { get;  }

        #endregion
    }
}
