namespace DotNetIpFilter.Services
{
    /// <summary>
    /// IpHostedService
    /// </summary>
    public class IpHostedService: IHostedService
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;
        /// <summary>
        ///  The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="IpHostedService"/> class.
        /// </summary>
        public IpHostedService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }

        /// <summary>
        /// IpHostedService StartAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = this.serviceProvider.CreateScope();
            var ipService = scope.ServiceProvider.GetRequiredService<IIpFilterService>();
            var ipAddresses = await ipService.GetAdminSafeIpList();
            this.configuration["AdminSafeIpList"] = ipAddresses;
        }

        /// <summary>
        /// IpHostedService StopAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
