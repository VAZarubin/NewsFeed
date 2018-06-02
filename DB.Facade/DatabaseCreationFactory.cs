using Org.Feeder.FeederDb;

namespace Facade
{
    public class DatabaseCreationFactory
    {
        #region Methods

        public Database Create()
        {
            /*TODO: Configuration Injection Required*/
            return new Database("local");
        }

        #endregion
    }
}
