using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Client.ServicesClient.Configuration;

namespace Client.Misc
{
    public class BackgroundLoader : IBackgroundLoader
    {
        #region Static and Readonly Fields

        private readonly IList<IDisposable> observerList;

        private readonly IObservable<long> scheduledTimer;

        #endregion

        #region Constructors

        public BackgroundLoader(IConfiguration configuration)
        {
            observerList = new List<IDisposable>();
            scheduledTimer = Observable.Interval(TimeSpan.FromSeconds(int.Parse(configuration.BackgroundWorkerPullTick)),
                DispatcherScheduler.Current);
        }

        #endregion

        #region IBackgroundLoader Members

        public void Dispose()
        {
            foreach (IDisposable disposable in observerList)
            {
                disposable.Dispose();
            }
        }

        public void RegisterObserver(IObserver<long> observer)
        {
            IDisposable createdObserver = scheduledTimer.Subscribe(observer);

            observerList.Add(createdObserver);
        }

        #endregion
    }
}
