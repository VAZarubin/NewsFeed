using System.Windows.Input;
using Caliburn.Micro;
using Client.Misc;

namespace Client.ViewModels
{
    public class IntroViewModel : Screen
    {
        #region Static and Readonly Fields

        private readonly INavigationService navigationService;

        #endregion

        #region Constructors

        public IntroViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        #endregion

        #region Properties

        public ICommand LaunchApplication => new ActionCommand((obj) => ProceedToNewsFeed());

        #endregion

        #region Methods

        public void ProceedToNewsFeed()
        {
            navigationService.For<PostListViewModel>().Navigate();
        }

        #endregion
    }
}
