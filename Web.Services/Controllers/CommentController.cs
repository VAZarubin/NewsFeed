using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using DAL;
using Models;

namespace Web.Services.Controllers
{
    public class CommentController : ApiController
    {
        #region Static and Readonly Fields

        private readonly ICommentRepository commentRepository;

        #endregion

        #region Constructors

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<Comment>> Get(int id)
        {
            IEnumerable<Comment> data = await commentRepository.GetCommentForPostAsync(id);

            return data;
        }

        #endregion
    }
}
