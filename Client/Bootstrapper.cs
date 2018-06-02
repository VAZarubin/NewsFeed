using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Client.Misc;
using Client.ServicesClient.ClientFactory;
using Client.ServicesClient.Configuration;
using Client.ServicesClient.Services.Comments;
using Client.ServicesClient.Services.Post;
using Client.ServicesClient.Services.PostSummaries;
using Client.ViewModels;

namespace Client
{
    public class Bootstrapper : BootstrapperBase
    {
        #region Fields

        private SimpleContainer container;

        #endregion

        #region Constructors

        public Bootstrapper()
        {
            Initialize();
        }

        #endregion

        #region Methods

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Instance(container);

            container.PerRequest<IPostService, PostService>();
            container.PerRequest<ICommentService, CommentService>();
            container.PerRequest<IPostSummariesService, PostSummariesService>();

            container.PerRequest<IConfiguration, Configuration>();
            container.PerRequest<IHttpClientFactory, HttpClientFactory>();


            container.PerRequest<IBackgroundLoader, BackgroundLoader>();

            container.Singleton<IWindowManager, WindowManager>().Singleton<IEventAggregator, EventAggregator>();

            container.PerRequest<ShellViewModel>()
                .PerRequest<IntroViewModel>()
                .PerRequest<PostListViewModel>()
                .PerRequest<PostViewModel>();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        #endregion

        #region Event Handling

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
        }

        #endregion
    }
}
