using System;
using System.Collections.ObjectModel;

namespace Client.Misc
{
    public class LoadingBar : IDisposable
    {
        #region Static and Readonly Fields

        private readonly ObservableCollection<LoadingBar> loadingBars;

        #endregion

        #region Constructors

        public LoadingBar(ObservableCollection<LoadingBar> loadingBars)
        {
            this.loadingBars = loadingBars;
            loadingBars.Add(this);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            loadingBars.Remove(this);
        }

        #endregion
    }
}
