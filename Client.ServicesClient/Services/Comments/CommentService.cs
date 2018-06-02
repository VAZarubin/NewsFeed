using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Client.ServicesClient.ClientFactory;
using Models;

namespace Client.ServicesClient.Services.Comments
{
    public class CommentService : ICommentService
    {
        #region Static and Readonly Fields

        private const string catalog = "api/comment";

        private readonly IHttpClientFactory httpClientFactory;

        #endregion

        #region Constructors

        public CommentService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        #endregion

        #region ICommentService Members

        public async Task<IEnumerable<Comment>> GetCommentForPost(int id)
        {
            using (HttpClient http = httpClientFactory.CreateClient())
            {
                HttpResponseMessage result = await http.GetAsync($"{catalog}/{id}");

                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<IEnumerable<Comment>>(new MediaTypeFormatter[] { new JsonMediaTypeFormatter(), });
            }
        }

        #endregion
    }
}
