using System.Collections.Generic;
using System.Linq;
using DB.Facade.Translators;
using Org.Feeder.FeederDb;

namespace DB.Facade.User
{
    internal class UserSource : IUserSource
    {
        #region Static and Readonly Fields

        private readonly Database context;

        private readonly ITranslator<Org.Feeder.FeederDb.User, Entites.User> userTranslator;

        #endregion

        #region Constructors

        public UserSource(Database context, ITranslator<Org.Feeder.FeederDb.User, Entites.User> userTranslator)
        {
            this.context = context;
            this.userTranslator = userTranslator;
        }

        #endregion

        #region IUserSource Members

        public IEnumerable<Entites.User> GetUsers()
        {
            IEnumerable<Org.Feeder.FeederDb.User> users = context.GetUsers();

            return users?.Select(x => userTranslator.Translate(x)).ToArray() ?? new Entites.User[0];
        }

        #endregion
    }
}
