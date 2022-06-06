using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            SystemProxyController.SetIEProxy(true, true, "localhost:2552", string.Empty);
            var p = System.Net.WebRequest.DefaultWebProxy?.GetProxy(new("https://github.com")) ?? throw new Exception();
            Assert.Equal("localhost", p.Host);
            Assert.Equal(2552, p.Port);
            SystemProxyController.ResetIEProxy();
        }
    }
}
