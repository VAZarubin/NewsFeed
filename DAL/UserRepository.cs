using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataSource.Users;
using Models;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        #region Static and Readonly Fields

        private readonly IUserService userService;

        #endregion

        #region Constructors

        public UserRepository(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region IUserRepository Members

        public async Task<User> GetUserById(int id)
        {
            IEnumerable<Entites.User> userList = await userService.GetUserList();

            Entites.User user = userList.Single(x => x.UserId.Id == id);

            return new User() { Email = user.Email, Name = user.Name, ImageUri = user.ImageUrl };
        }

        public async Task<IEnumerable<User>> GetUserList()
        {
            IEnumerable<Entites.User> data = await userService.GetUserList();

            return data.Select(x => new User() { Email = x.Email, Name = x.Name, ImageUri = x.ImageUrl, });
        }

        #endregion
    }
}
