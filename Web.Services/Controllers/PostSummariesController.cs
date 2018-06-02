using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using DAL;
using Models;

namespace Web.Services.Controllers
{
    public class PostSummariesController : ApiController
    {
        #region Static and Readonly Fields

        private readonly IPostRepository postRepository;

        #endregion

        #region Constructors

        public PostSummariesController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<PostSummary>> Get()
        {
            return await postRepository.GetPostList();
        }

        #endregion
    }
}
