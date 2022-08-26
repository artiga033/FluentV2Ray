namespace FluentV2Ray.Interop.Model
{
    /// <summary>
    /// <para> Browser Forwarder uses browser page foward to forward supported traffic.</para>
    /// <see href="https://www.v2fly.org/config/browserforwarder.html#browserforwarderobject"/>
    /// </summary>
    public class BrowserForwarderObject : IV2RayConfig
    {
        public string? ListenAddr { get; set; }
        public string? ListenPort { get; set; }
        public BrowserForwarderObject() { }
        public BrowserForwarderObject(string listenAddr, string listenPort)
        {
            this.ListenAddr = listenAddr;
            this.ListenPort = listenPort;
        }
    }
}
