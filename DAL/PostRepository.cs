using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataSource.Posts;
using Entites.Contracts;
using Models;

namespace DAL
{
    public class PostRepository : IPostRepository
    {
        #region Static and Readonly Fields

        private readonly IPostService postService;
        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors

        public PostRepository(IPostService postService, IUserRepository userRepository)
        {
            this.postService = postService;
            this.userRepository = userRepository;
        }

        #endregion

        #region IPostRepository Members

        public async Task<Post> GetPostById(int id)
        {
            Entites.Post post = await postService.GetPostById(new PostId(id));

            User user = await userRepository.GetUserById(post.UserId.Id);

            return new Post() { Author = user, Body = post.Body, Title = post.Title, };
        }

        public async Task<IEnumerable<PostSummary>> GetPostList()
        {
            IEnumerable<Entites.PostSummary> data = await postService.GetPostSummaries();

            return data.Select(x => new PostSummary() { Id = x.PostId.Id, Title = x.Title });
        }

        #endregion
    }
}
