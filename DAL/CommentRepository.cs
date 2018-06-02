using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataSource.Comments;
using Entites.Contracts;
using Models;

namespace DAL
{
    public class CommentRepository : ICommentRepository
    {
        #region Static and Readonly Fields

        private readonly ICommentService commentService;
        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors

        public CommentRepository(IUserRepository userRepository, ICommentService commentService)
        {
            this.userRepository = userRepository;
            this.commentService = commentService;
        }

        #endregion

        #region ICommentRepository Members

        public async Task<IEnumerable<Comment>> GetCommentForPostAsync(int postId)
        {
            IEnumerable<Entites.Comment> commentList = await commentService.GetPostCommentList(new PostId(postId));

            IEnumerable<User> userList = await userRepository.GetUserList();

            return
                commentList.Select(
                                   comment =>
                                       new Comment()
                                       {
                                           Text = comment.Text,
                                           User = userList.First(x => x.Email == comment.CommenterEmail)
                                       });
        }

        #endregion
    }
}
