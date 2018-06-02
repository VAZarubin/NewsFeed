using System;

namespace Client.Misc
{
    public interface IBackgroundLoader : IDisposable
    {
        #region Methods

        void RegisterObserver(IObserver<long> observer);

        #endregion
    }
}
