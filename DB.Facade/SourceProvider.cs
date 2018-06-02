using DB.Facade.Comment;
using DB.Facade.Post;
using DB.Facade.Translators;
using DB.Facade.User;
using Org.Feeder.FeederDb;
using SimpleInjector;

namespace DB.Facade
{
    public class SourceProvider
    {
        #region Static Methods

        public static ICommentSource CommentSource(Container parentContainer)
        {
            var container = new Container();

            container.Register(parentContainer.GetInstance<Database>);
            container.Register<ITranslator<Org.Feeder.FeederDb.Comment, Entites.Comment>, CommentTranslator>();
            container.Register<ICommentSource, CommentSource>();

            container.Verify();

            return container.GetInstance<ICommentSource>();
        }

        public static IPostSource GetPostSource(Container parentContainer)
        {
            var container = new Container();

            container.Register(parentContainer.GetInstance<Database>);

            container.Register<ITranslator<Org.Feeder.FeederDb.Post, Entites.Post>, PostTranslator>();
            container.Register<ITranslator<PostSummary, Entites.PostSummary>, PostSummaryTranslator>();

            container.Register<IPostSource, PostSource>();

            container.Verify();

            return container.GetInstance<IPostSource>();
        }

        public static IUserSource GetUserSource(Container parentContainer)
        {
            var container = new Container();

            container.Register(parentContainer.GetInstance<Database>);

            container.Register<ITranslator<Org.Feeder.FeederDb.User, Entites.User>, UserTranslator>();
            container.Register<IUserSource, UserSource>();

            container.Verify();

            return container.GetInstance<IUserSource>();
        }

        #endregion
    }
}
