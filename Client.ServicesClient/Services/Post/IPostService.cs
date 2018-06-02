using System.Threading.Tasks;

namespace Client.ServicesClient.Services.Post
{
    public interface IPostService
    {
        #region Methods

        Task<Models.Post> GetPostById(int id);

        #endregion
    }
}
