using Entites.Contracts;
using Org.Feeder.FeederDb;

namespace DB.Facade.Translators
{
    internal class PostSummaryTranslator : ITranslator<PostSummary, Entites.PostSummary>
    {
        #region ITranslator<PostSummary,PostSummary> Members

        public Entites.PostSummary Translate(PostSummary source)
        {
            if (source == null)
            {
                return null;
            }

            return new Entites.PostSummary() { PostId = new PostId(source.Id), Title = source.Title };
        }

        #endregion
    }
}
