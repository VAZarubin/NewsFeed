using System;
using System.Threading.Tasks;

namespace Client.Misc
{
    public class BackgroundObserver<T> : IObserver<long>
    {
        #region Static and Readonly Fields

        private readonly Func<Task<T>> loadAction;
        private readonly Action<T> setAction;

        #endregion

        #region Constructors

        public BackgroundObserver(Func<Task<T>> loadAction, Action<T> setAction)
        {
            this.loadAction = loadAction;
            this.setAction = setAction;
        }

        #endregion

        #region IObserver<long> Members

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
        }

        public async void OnNext(long value)
        {
            T data = await loadAction.Invoke();
            
            setAction.Invoke(data);
        }

        #endregion
    }
}
