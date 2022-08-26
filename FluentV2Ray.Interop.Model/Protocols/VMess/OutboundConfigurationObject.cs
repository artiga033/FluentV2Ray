namespace FluentV2Ray.Interop.Model.Protocols.VMess
{
    public class OutboundConfigurationObject : OutboundConfigurationObjectBase
    {
        public IList<ServerObject> Vnext { get; set; } = new List<ServerObject>();
        public OutboundConfigurationObject() { Vnext.Add(new ServerObject() { Users = {new("")}}); }
        public OutboundConfigurationObject(string address, int port, string id)
        {
            this.Vnext = new List<ServerObject>() { new(address, port, id) };
        }
    }
}
