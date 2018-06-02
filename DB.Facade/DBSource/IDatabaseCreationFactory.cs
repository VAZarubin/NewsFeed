using Org.Feeder.FeederDb;

namespace DB.Facade.DBSource
{
    public interface IDatabaseCreationFactory
    {
        #region Methods

        Database Create();

        #endregion
    }
}
