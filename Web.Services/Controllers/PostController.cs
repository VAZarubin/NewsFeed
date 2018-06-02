using System.Threading.Tasks;
using System.Web.Http;
using DAL;
using Models;

namespace Web.Services.Controllers
{
    public class PostController : ApiController
    {
        #region Static and Readonly Fields

        private readonly IPostRepository postRepository;

        #endregion

        #region Constructors

        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        #endregion

        #region Methods

        public async Task<Post> Get(int id)
        {
            return await postRepository.GetPostById(id);
        }

        #endregion
    }
}
