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
            // Sample IP addresses, this is where you get all IP addresses from your database or services using an async call.
            // Added ::1 for localhost. 
            var ipListArray = "127.0.0.1;192.168.1.5;128.0.0.1/32;128.168.1.5/32;::1"; 
            this.logger.LogInformation($"{nameof(IpFilterService)}.{nameof(GetAdminSafeIpList)} end");
            return ipListArray;
        }
    }
}
