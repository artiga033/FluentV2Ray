using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xunit;
using Xunit.Abstractions;

namespace FluentV2Ray.Controller.Tests
{
    public class SystemProxyControllerTest
    {
        private readonly ITestOutputHelper _output;

        public SystemProxyControllerTest(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void SetGlobalProxy_Success()
        {
            SystemProxyController sysproxyController = new SystemProxyController(new CoreConfigController(NullLogger<CoreConfigController>.Instance));
            sysproxyController.SetIEProxy(true, true, "localhost:2552", string.Empty);
            var p = System.Net.WebRequest.DefaultWebProxy?.GetProxy(new("https://github.com")) ?? throw new Exception();
            Assert.Equal("localhost", p.Host);
            Assert.Equal(2552, p.Port);
            sysproxyController.ResetIEProxy();
        }
    }
}
