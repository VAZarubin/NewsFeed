using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Client.Misc;
using Client.ServicesClient.Services.PostSummaries;
using Models;

namespace Client.ViewModels
{
    public class PostListViewModel : Screen, INotifyPropertyChanged
    {
        #region Static and Readonly Fields

        private readonly BackgroundObserver<IEnumerable<PostSummary>> backgroundObserver;

        private readonly IBackgroundLoader loader;

        private readonly INavigationService navigationService;
        private readonly IPostSummariesService postSummariesService;
        private readonly IProgressBarController progressBarController;
        private readonly IWindowManager windowManager;

        #endregion

        #region Fields

        private ObservableCollection<PostSummary> postList;

        private string searchText;

        #endregion

        #region Constructors

        public PostListViewModel(INavigationService navigationService,
            IPostSummariesService postSummariesService,
            IProgressBarController progressBarController,
            IWindowManager windowManager,
            IBackgroundLoader loader)
        {
            this.navigationService = navigationService;
            this.postSummariesService = postSummariesService;
            this.progressBarController = progressBarController;
            this.windowManager = windowManager;
            this.loader = loader;

            backgroundObserver =
                new BackgroundObserver<IEnumerable<PostSummary>>(async () => await postSummariesService.GetPostSummaries(),
                    (data) =>
                    {
                        if (OriginList.Count != Enumerable.Count(data))
                        {
                            OriginList = new ObservableCollection<PostSummary>(data);
                        }
                    });
        }

        #endregion

        #region Properties

        public ICommand NavigateToPost => new ActionCommand(NavigateTo);

        public IReadOnlyCollection<PostSummary> OriginList { get; set; }

        public ObservableCollection<PostSummary> PostList
        {
            get
            {
                return postList;
            }
            set
            {
                if (value == postList)
                {
                    return;
                }

                postList = value;

                NotifyOfPropertyChange(nameof(PostList));
            }
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                if (searchText == value)
                {
                    return;
                }

                searchText = value;
                NotifyOfPropertyChange(searchText);
                FilterBy();
            }
        }

        #endregion

        #region Methods

        private async void Retry()
        {
            await LoadData();
        }

        protected override async void OnActivate()
        {
            try
            {
                using (progressBarController.Show())
                {
                    await LoadData();
                }
            }
            catch (Exception)
            {
                windowManager.ShowDialog(new DialogViewModel()
                {
                    CallbackAction = Retry
                });
            }

            loader.RegisterObserver(backgroundObserver);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            loader.Dispose();
        }

        private void FilterBy()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                PostList = new ObservableCollection<PostSummary>(OriginList);
                return;
            }
            PostList =
                new ObservableCollection<PostSummary>(
                    OriginList.Where(x => x.Title.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0));
        }

        private async Task LoadData()
        {
            IEnumerable<PostSummary> list = await postSummariesService.GetPostSummaries();

            OriginList = new List<PostSummary>(list);

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                PostList = new ObservableCollection<PostSummary>(OriginList);
            }
        }

        private void NavigateTo(object o)
        {
            var arg = (int)o;

            navigationService.For<PostViewModel>().WithParam(x => x.PostId, arg).Navigate();
        }

        #endregion
    }
}
