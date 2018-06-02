using System.Configuration;

namespace Client.ServicesClient.Configuration
{
    public class Configuration : IConfiguration
    {
        #region Constructors

        public Configuration()
        {
            BackgroundWorkerPullTick = ConfigurationManager.AppSettings["BackgroundWorkerPullTick"];
            UriAddress = ConfigurationManager.AppSettings["Uri"];
        }

        #endregion

        #region IConfiguration Members

        public string BackgroundWorkerPullTick { get; set; }

        public string UriAddress { get; set; }

        #endregion
    }
}
