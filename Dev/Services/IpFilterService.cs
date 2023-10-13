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
            // Sample IP addresses, this is where we make an async call to get the IP addresses
            // Adding ::1 it for localhost
            var ipListArray = "111.88.180.140;111.78.156.140;120.88.180.140/32;130.78.156.140/32;::1"; 
            this.logger.LogInformation($"{nameof(IpFilterService)}.{nameof(GetAdminSafeIpList)} end");
            return ipListArray;
        }
    }
}
