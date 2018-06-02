namespace Models
{
    public class Post
    {
        #region Properties

        public User Author { get; set; }

        public string Body { get; set; }

        public int Id { get; set; }
        public string Title { get; set; }

        #endregion
    }
}
