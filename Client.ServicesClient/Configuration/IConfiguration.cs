namespace Client.ServicesClient.Configuration
{
    public interface IConfiguration
    {
        #region Properties

        string BackgroundWorkerPullTick { get; }
        string UriAddress { get; }

        #endregion
    }
}
