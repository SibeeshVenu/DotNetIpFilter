namespace DotNetIpFilter.Services
{
    /// <summary>
    /// IpFilterService
    /// </summary>
    public class IpFilterService : IIpFilterService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<IpFilterService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="IpFilterService"/> class.
        /// </summary>
        public IpFilterService(ILogger<IpFilterService> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// GetAdminSafeIpList
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAdminSafeIpList()
        {
            this.logger.LogInformation($"{nameof(IpFilterService)}.{nameof(GetAdminSafeIpList)} start");
            // To mimic an async call, this can be your API to get the IP addresses
            await Task.Delay(1000);
            var ipListArray =  "111.88.180.140;111.78.156.140"; // Sample IP addresses
            this.logger.LogInformation($"{nameof(IpFilterService)}.{nameof(GetAdminSafeIpList)} end");
            return ipListArray;
        }
    }
}
