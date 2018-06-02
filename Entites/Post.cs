using Entites.Contracts;

namespace Entites
{
    public class Post
    {
        #region Properties

        public string Body { get; set; }

        public PostId PostId { get; set; }

        public string Title { get; set; }

        public UserId UserId { get; set; }

        #endregion
    }
}
