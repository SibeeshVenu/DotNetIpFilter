using System.Net;
using NSubstitute;
using Microsoft.Extensions.Logging;
using DotNetIpFilter.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetIpFilter.Test
{
    [TestClass]
    public class IpActionFilterTest
    {
        private readonly ILogger<IpActionFilter> logger;

        public IpActionFilterTest()
        {
            this.logger = Substitute.For<ILogger<IpActionFilter>>();
        }

        [TestMethod]
        public void IpActionFilterTestWhenValidIp()
        {
            var counter = 0;
            var validIps = "120.78.179.140;111.78.179.170";
            var IpFilterService = Substitute.For<IIpFilterService>();
            IpFilterService.When(x => x.GetAdminSafeIpList()).Do(x => counter++);
            IpFilterService.GetAdminSafeIpList().Returns(validIps);

            var incomingIp = IPAddress.Parse("120.78.179.140");
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(
                   Substitute.For<HttpContext>(),
                   Substitute.For<RouteData>(),
                   Substitute.For<ActionDescriptor>(),
                   modelState
               );

            var actionExecutingContext = new ActionExecutingContext(
                   actionContext,
                   new List<IFilterMetadata>(),
                   new Dictionary<string, object>(),
                   Substitute.For<Controller>()
               );
            actionExecutingContext.HttpContext.Connection.RemoteIpAddress = incomingIp;

            var attrib = new IpActionFilter(validIps, this.logger);
            attrib.OnActionExecuting(actionExecutingContext);
            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void IpActionFilterTestWhenInValidIp()
        {
            var validIps = "111.78.179.140;111.78.179.170";
            var IpFilterService = Substitute.For<IIpFilterService>();
            IpFilterService.GetAdminSafeIpList().Returns(validIps);

            var incomingIp = IPAddress.Parse("120.78.179.140");
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(
                   Substitute.For<HttpContext>(),
                   Substitute.For<RouteData>(),
                   Substitute.For<ActionDescriptor>(),
                   modelState
               );

            var actionExecutingContext = new ActionExecutingContext(
                   actionContext,
                   new List<IFilterMetadata>(),
                   new Dictionary<string, object>(),
                   Substitute.For<Controller>()
               );
            actionExecutingContext.HttpContext.Connection.RemoteIpAddress = incomingIp;

            var attrib = new IpActionFilter(validIps, this.logger);
            attrib.OnActionExecuting(actionExecutingContext);
            var statusCode = (actionExecutingContext.Result as StatusCodeResult).StatusCode;
            Assert.AreEqual(statusCode, 403);
        }
    }
}