using System.Collections.Generic;
using System.Threading.Tasks;
using Entites;

namespace DataSource.Users
{
    public interface IUserService
    {
        #region Methods

        Task<IEnumerable<User>> GetUserList();

        #endregion
    }
}
