using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;
using Caliburn.Micro;
using Client.Misc;

namespace Client.ViewModels
{
    public class ShellViewModel : Screen, IProgressBarController
    {
        #region Static and Readonly Fields

        private readonly SimpleContainer container;

        #endregion

        #region Fields

        private bool busy;
        private INavigationService navigationService;

        #endregion

        #region Constructors

        public ShellViewModel(SimpleContainer container)
        {
            this.container = container;
            LoadBars = new ObservableCollection<LoadingBar>();
            LoadBars.CollectionChanged += OnProgressBarTokensCollectionChanged;
        }

        #endregion

        #region Properties

        public bool IsBusy
        {
            get
            {
                return busy;
            }
            set
            {
                busy = value;
                NotifyOfPropertyChange(nameof(IsBusy));
            }
        }

        private ObservableCollection<LoadingBar> LoadBars { get; }

        #endregion

        #region Methods

        public void RegisterFrame(Frame frame)
        {
            navigationService = new FrameAdapter(frame);

            container.Instance(navigationService);

            container.Instance<IProgressBarController>(this);

            navigationService.For<IntroViewModel>().Navigate();
        }

        #endregion

        #region IProgressBarController Members

        public LoadingBar Show()
        {
            return new LoadingBar(LoadBars);
        }

        #endregion

        #region Event Handling

        private void OnProgressBarTokensCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (LoadBars.Count > 0)
            {
                IsBusy = true;
            }

            if (LoadBars.Count == 0)
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
