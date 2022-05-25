using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentV2Ray.Interop.Model
{
    /// <summary>
    /// <para> Browser Forwarder uses browser page foward to forward supported traffic.</para>
    /// <see href="https://www.v2fly.org/config/browserforwarder.html#browserforwarderobject"/>
    /// </summary>
    public class BrowserForwarderObject
    {
        public string? ListenAddr { get; set; }
        public string? ListenPort { get; set; }
        public BrowserForwarderObject(string listenAddr, string listenPort)
        {
            this.ListenAddr = listenAddr;
            this.ListenPort = listenPort;
        }
        public BrowserForwarderObject()
        {
            this.ListenAddr = null;
            this.ListenPort = null;
        }
    }
}
