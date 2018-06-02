using Entites.Contracts;

namespace DB.Facade.Translators
{
    internal class CommentTranslator : ITranslator<Org.Feeder.FeederDb.Comment, Entites.Comment>
    {
        #region ITranslator<Comment,Comment> Members

        public Entites.Comment Translate(Org.Feeder.FeederDb.Comment source)
        {
            if (source == null)
            {
                return null;
            }

            return new Entites.Comment()
            {
                PostId = new PostId(source.PostId),
                CommenterEmail = source.CommenterEmail,
                CommenterName = source.CommenterName,
                Text = source.Text
            };
        }

        #endregion
    }
}
