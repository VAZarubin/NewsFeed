using System.Collections.Generic;

namespace DB.Facade.User
{
    public interface IUserSource
    {
        #region Methods

        IEnumerable<Entites.User> GetUsers();

        #endregion
    }
}
