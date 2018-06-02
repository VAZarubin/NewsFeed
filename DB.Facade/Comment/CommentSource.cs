using System.Collections.Generic;
using System.Linq;
using DB.Facade.Translators;
using Entites.Contracts;
using Org.Feeder.FeederDb;

namespace DB.Facade.Comment
{
    internal class CommentSource : ICommentSource
    {
        #region Static and Readonly Fields

        private readonly ITranslator<Org.Feeder.FeederDb.Comment, Entites.Comment> commentTranslator;
        private readonly Database context;

        #endregion

        #region Constructors

        public CommentSource(Database context, ITranslator<Org.Feeder.FeederDb.Comment, Entites.Comment> commentTranslator)
        {
            this.context = context;
            this.commentTranslator = commentTranslator;
        }

        #endregion

        #region ICommentSource Members

        public IEnumerable<Entites.Comment> GetComments(PostId postId)
        {
            IList<Org.Feeder.FeederDb.Comment> postComments = context.GetComments(postId.Id);

            return postComments?.Select(x => commentTranslator.Translate(x)).ToArray() ?? new Entites.Comment[0];
        }

        #endregion
    }
}
