using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Client.ServicesClient.Services.PostSummaries
{
    public interface IPostSummariesService
    {
        #region Methods

        Task<IEnumerable<PostSummary>> GetPostSummaries();

        #endregion
    }
}
