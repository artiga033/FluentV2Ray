namespace FluentV2Ray.Interop.Model.Protocols.Trojan
{
    public class OutboundConfigurationObject : OutboundConfigurationObjectBase
    {
        public IList<ServerObject> Servers { get; set; } = new List<ServerObject>();
        public OutboundConfigurationObject() { }
        public OutboundConfigurationObject(string address, int port, string password)
        {
            Servers = new List<ServerObject>()
            {
                new(address, port, password),
            };
        }
    }
}
