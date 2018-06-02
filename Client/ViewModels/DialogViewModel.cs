using System.Windows.Input;
using Caliburn.Micro;
using Client.Misc;
using Action = System.Action;

namespace Client.ViewModels
{
    public class DialogViewModel : Screen
    {
        #region Properties

        public Action CallbackAction { get; set; }

        public ICommand DismissCommand => new ActionCommand((obj) => Dismiss());

        #endregion

        #region Methods

        private void Dismiss()
        {
            CallbackAction?.Invoke();

            TryClose();
        }

        #endregion
    }
}
