using System;
using System.Configuration;

namespace Configurations
{
    public class NewsFeedConfiguration : INewsFeedConfiguration
    {
        #region Static Methods

        public static NewsFeedConfiguration Read()
        {
            return new NewsFeedConfiguration()
            {
                DatabaseConnection = ConfigurationManager.AppSettings["DatabaseConnection"],
                UserInvalidationTime = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["UserInvalidationTime"])),
                PostInvalidationTime = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["PostInvalidationTime"])),
                CommentInvalidationTime = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["CommentInvalidationTime"])),
                SummariesInvalidationTime = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["SummariesInvalidationTime"]))
            };
        }

        #endregion

        #region INewsFeedConfiguration Members

        public TimeSpan CommentInvalidationTime { get; private set; }

        public string DatabaseConnection { get; private set; }
        public TimeSpan PostInvalidationTime { get; private set; }
        public TimeSpan UserInvalidationTime { get; private set; }
        public TimeSpan SummariesInvalidationTime { get; private set; }

        #endregion
    }
}
