namespace FluentV2Ray.Interop.Model.Protocols.Shadowsocks
{
    public class OutboundConfigurationObject : OutboundConfigurationObjectBase
    {
        public IList<ServerObject> Servers { get; set; } = new List<ServerObject>();

        public OutboundConfigurationObject()
        { }
        public OutboundConfigurationObject(string address, int port, string method, string password)
        {
            Servers = new List<ServerObject>()
            {
                new ServerObject(address, port, method, password),
            };
        }
    }
}
