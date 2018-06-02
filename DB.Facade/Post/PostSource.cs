using System.Collections.Generic;
using System.Linq;
using DB.Facade.Translators;
using Entites.Contracts;
using Org.Feeder.FeederDb;

namespace DB.Facade.Post
{
    internal class PostSource : IPostSource
    {
        #region Static and Readonly Fields

        private readonly Database context;

        private readonly ITranslator<PostSummary, Entites.PostSummary> postSummaryTranslator;
        private readonly ITranslator<Org.Feeder.FeederDb.Post, Entites.Post> postTranslator;

        #endregion

        #region Constructors

        public PostSource(Database context,
            ITranslator<PostSummary, Entites.PostSummary> postSummaryTranslator,
            ITranslator<Org.Feeder.FeederDb.Post, Entites.Post> postTranslator)
        {
            this.context = context;
            this.postSummaryTranslator = postSummaryTranslator;
            this.postTranslator = postTranslator;
        }

        #endregion

        #region IPostSource Members

        public Entites.Post GetPost(PostId postId)
        {
            Org.Feeder.FeederDb.Post post = context.GetPost(postId.Id);

            return post == null ? null : postTranslator.Translate(post);
        }

        public IEnumerable<Entites.PostSummary> GetPostSummaries()
        {
            IEnumerable<PostSummary> postSummaries = context.GetPostSummaries();

            return postSummaries?.Select(x => postSummaryTranslator.Translate(x)).ToArray() ?? new Entites.PostSummary[0];
        }

        #endregion
    }
}
