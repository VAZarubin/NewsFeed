using System.Web;
using System.Web.Http;
using Configurations;
using DAL;
using DataSource.Comments;
using DataSource.Posts;
using DataSource.Users;
using DB.Facade.DBSource;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace Web.Services
{
    public class WebApiApplication : HttpApplication
    {
        #region Methods

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var configurationContainer = new Container();
            configurationContainer.RegisterSingleton<INewsFeedConfiguration>(NewsFeedConfiguration.Read);
            configurationContainer.RegisterSingleton<IDatabaseCreationFactory, DatabaseCreationFactory>();
            configurationContainer.Verify();

            var dbContainer = new Container();
            dbContainer.RegisterInstance(configurationContainer.GetInstance<IDatabaseCreationFactory>().Create());

            container.RegisterInstance(PostServiceCreator.CreatePostService(dbContainer, configurationContainer));
            container.RegisterInstance(UserServiceCreator.CreateUserService(dbContainer, configurationContainer));
            container.RegisterInstance(CommentServiceCreator.CreateCommentService(dbContainer, configurationContainer));

            container.RegisterSingleton<ICommentRepository, CommentRepository>();
            container.RegisterSingleton<IUserRepository, UserRepository>();
            container.RegisterSingleton<IPostRepository, PostRepository>();

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        #endregion
    }
}
