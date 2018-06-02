using Entites.Contracts;

namespace Entites
{
    public class User
    {
        #region Properties

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public UserId UserId { get; set; }

        #endregion
    }
}
