using Entites.Contracts;

namespace Entites
{
    public class Comment
    {
        #region Properties

        public string CommenterEmail { get; set; }

        public string CommenterName { get; set; }

        public PostId PostId { get; set; }

        public string Text { get; set; }

        #endregion
    }
}
