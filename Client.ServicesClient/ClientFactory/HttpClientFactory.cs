using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Client.ServicesClient.Configuration;

namespace Client.ServicesClient.ClientFactory
{
    public class HttpClientFactory : IHttpClientFactory
    {
        #region Static and Readonly Fields

        private readonly IConfiguration configuration;

        #endregion

        #region Constructors

        public HttpClientFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        #endregion

        #region IHttpClientFactory Members

        public HttpClient CreateClient()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(configuration.UriAddress);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        #endregion
    }
}
