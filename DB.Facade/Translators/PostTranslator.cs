using Entites.Contracts;

namespace DB.Facade.Translators
{
    internal class PostTranslator : ITranslator<Org.Feeder.FeederDb.Post, Entites.Post>
    {
        #region ITranslator<Post,Post> Members

        public Entites.Post Translate(Org.Feeder.FeederDb.Post source)
        {
            if (source == null)
            {
                return null;
            }

            return new Entites.Post()
            {
                Body = source.Body,
                PostId = new PostId(source.Id),
                Title = source.Title,
                UserId = new UserId(source.UserId)
            };
        }

        #endregion
    }
}
