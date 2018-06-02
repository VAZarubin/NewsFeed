using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Client.ServicesClient.ClientFactory;

namespace Client.ServicesClient.Services.Post
{
    public class PostService : IPostService
    {
        #region Constants

        private const string catalog = "api/post";

        #endregion

        #region Static and Readonly Fields

        private readonly IHttpClientFactory httpClientFactory;

        #endregion

        #region Constructors

        public PostService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        #endregion

        #region IPostService Members

        public async Task<Models.Post> GetPostById(int id)
        {
            using (HttpClient http = httpClientFactory.CreateClient())
            {
                HttpResponseMessage result = await http.GetAsync($"{catalog}/{id}");

                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<Models.Post>(new MediaTypeFormatter[] { new JsonMediaTypeFormatter(), });
            }
        }

        #endregion
    }
}
