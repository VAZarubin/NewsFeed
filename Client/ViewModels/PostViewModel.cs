using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Client.Misc;
using Client.ServicesClient.Services.Comments;
using Client.ServicesClient.Services.Post;
using Models;

namespace Client.ViewModels
{
    public class PostViewModel : Screen
    {
        #region Static and Readonly Fields

        private readonly IBackgroundLoader backgroundLoader;

        private readonly BackgroundObserver<IEnumerable<Comment>> backgroundObserver;

        private readonly ICommentService commentService;

        private readonly INavigationService navigationService;

        private readonly IPostService postService;
        private readonly IProgressBarController progressBarController;
        private readonly IWindowManager windowManager;

        #endregion

        #region Fields

        private ObservableCollection<Comment> commentList;

        private Post post;

        #endregion

        #region Constructors

        public PostViewModel(ICommentService commentService,
            IPostService postService,
            INavigationService navigationService,
            IWindowManager windowManager,
            IProgressBarController progressBarController,
            IBackgroundLoader backgroundLoader)
        {
            this.commentService = commentService;
            this.postService = postService;
            this.navigationService = navigationService;
            this.windowManager = windowManager;
            this.progressBarController = progressBarController;
            this.backgroundLoader = backgroundLoader;

            backgroundObserver = new BackgroundObserver<IEnumerable<Comment>>(async () => await commentService.GetCommentForPost(PostId),
                (data) =>
                {
                    if (data.Count() != CommentList.Count)
                    {
                        CommentList = new ObservableCollection<Comment>(data);
                    }
                });
        }

        #endregion

        #region Properties

        public string Body => Post.Body;

        public ObservableCollection<Comment> CommentList
        {
            get
            {
                return commentList;
            }
            set
            {
                if (value == commentList)
                {
                    return;
                }

                commentList = value;
                NotifyOfPropertyChange(nameof(CommentList));
            }
        }

        public ICommand GoBack => new ActionCommand((obj) => GoToNewsFeed());

        public Post Post
        {
            get
            {
                return post;
            }
            set
            {
                if (value == post)
                {
                    return;
                }

                post = value;
                NotifyOfPropertyChange(nameof(Post));
                NotifyOfPropertyChange(nameof(Title));
                NotifyOfPropertyChange(nameof(Body));
            }
        }

        public int PostId { get; set; }

        public string Title => Post.Title;

        #endregion

        #region Methods

        protected override async void OnActivate()
        {
            try
            {
                using (progressBarController.Show())
                {
                    await GetPost();
                }
            }
            catch (Exception)
            {
                windowManager.ShowDialog(new DialogViewModel() { CallbackAction = () => navigationService.GoBack() });
            }

            await GetCommentList();

            backgroundLoader.RegisterObserver(backgroundObserver);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            backgroundLoader.Dispose();
        }

        private async Task GetCommentList()
        {
            try
            {
                CommentList = new ObservableCollection<Comment>(await commentService.GetCommentForPost(PostId));
            }
            catch (Exception)
            {
                CommentList = new ObservableCollection<Comment>();
            }
        }

        private async Task GetPost()
        {
            Post = await postService.GetPostById(PostId);
        }

        private void GoToNewsFeed()
        {
            navigationService.For<PostListViewModel>().Navigate();
        }

        #endregion
    }
}
