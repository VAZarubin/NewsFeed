using Configurations;
using Org.Feeder.FeederDb;

namespace DB.Facade.DBSource
{
    public class DatabaseCreationFactory : IDatabaseCreationFactory
    {
        #region Static and Readonly Fields

        private readonly INewsFeedConfiguration configuration;

        #endregion

        #region Constructors

        public DatabaseCreationFactory(INewsFeedConfiguration configuration)
        {
            this.configuration = configuration;
        }

        #endregion

        #region IDatabaseCreationFactory Members

        public Database Create()
        {
            return new Database(configuration.DatabaseConnection);
        }

        #endregion
    }
}
