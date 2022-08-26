namespace FluentV2Ray.Interop.Model.Protocols.Http
{
    public class OutboundConfigurationObject : OutboundConfigurationObjectBase
    {
        public IList<ServerObject> Servers { get; set; } = new List<ServerObject>();
        public OutboundConfigurationObject() { }
        public OutboundConfigurationObject(string address, int port)
        {
            Servers = new List<ServerObject>() { new ServerObject(address, port) };
        }
    }
}
