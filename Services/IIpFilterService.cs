namespace DotNetIpFilter.Services
{
    /// <summary>
    /// IIpFilterService
    /// </summary>
    public interface IIpFilterService
    {
        /// <summary>
        /// GetAdminSafeIpList
        /// </summary>
        /// <returns></returns>
        Task<string> GetAdminSafeIpList();
    }
}
