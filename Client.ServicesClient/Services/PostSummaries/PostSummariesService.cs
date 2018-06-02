using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Client.ServicesClient.ClientFactory;
using Models;

namespace Client.ServicesClient.Services.PostSummaries
{
    public class PostSummariesService : IPostSummariesService
    {
        #region Constants

        private const string catalog = "/api/postsummaries";

        #endregion

        #region Static and Readonly Fields

        private readonly IHttpClientFactory httpClientFactory;

        #endregion

        #region Constructors

        public PostSummariesService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        #endregion

        #region IPostSummariesService Members

        public async Task<IEnumerable<PostSummary>> GetPostSummaries()
        {
            using (HttpClient http = httpClientFactory.CreateClient())
            {
                HttpResponseMessage response = await http.GetAsync(catalog);
                response.EnsureSuccessStatusCode();

                return
                    await
                        response.Content.ReadAsAsync<IEnumerable<PostSummary>>(new MediaTypeFormatter[] { new JsonMediaTypeFormatter(), });
            }
        }

        #endregion
    }
}
