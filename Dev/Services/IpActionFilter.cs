using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DotNetIpFilter.Services
{
    /// <summary>
    /// IpActionFilter
    /// </summary>
    public class IpActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Admin safe ip list
        /// </summary>
        private readonly string safelist;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<IpActionFilter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="IpActionFilter"/> class.
        /// </summary>
        public IpActionFilter(string safelist, ILogger<IpActionFilter> logger)
        {
            this.safelist = safelist;
            this.logger = logger;
        }

        /// <summary>
        /// IpActionFilter OnActionExecuting
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentException"></exception>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (context == null) { throw new ArgumentException($"{nameof(IpActionFilter)}.{nameof(OnActionExecuting)} {nameof(context)} is null"); }
                var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
                if (remoteIp == null) { throw new ArgumentException($"{nameof(IpActionFilter)}.{nameof(OnActionExecuting)} {nameof(remoteIp)}  is null"); }

                var ipAddresses = this.safelist.Split(';');
                var badIp = true;

                if (remoteIp.IsIPv4MappedToIPv6)
                {
                    remoteIp = remoteIp.MapToIPv4();
                }

                foreach (var address in ipAddresses)
                {
                    // if it has / character, it is a range
                    if (address.Contains('/'))
                    {
                        var splitPrefix = address.Split('/');
                        var ipValidator = new IPNetwork(IPAddress.Parse(splitPrefix[0]), Convert.ToInt16(splitPrefix[1]));
                        var res = ipValidator.Contains(remoteIp);

                        if (res)
                        {
                            badIp = false;
                            break;
                        }

                    }
                    else
                    {
                        var curIp = IPAddress.Parse(address);
                        if (curIp.Equals(remoteIp))
                        {
                            badIp = false;
                            break;
                        }
                    }
                }

                if (badIp)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                    return;
                }

                base.OnActionExecuting(context);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
