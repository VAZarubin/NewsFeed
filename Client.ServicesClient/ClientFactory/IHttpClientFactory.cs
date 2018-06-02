using System.Net.Http;

namespace Client.ServicesClient.ClientFactory
{
    public interface IHttpClientFactory
    {
        #region Methods

        HttpClient CreateClient();

        #endregion
    }
}
