using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public interface IUserRepository
    {
        #region Methods

        Task<User> GetUserById(int id);

        Task<IEnumerable<User>> GetUserList();

        #endregion
    }
}
