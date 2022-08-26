namespace FluentV2Ray.Interop.Model.Protocols.Vless
{
    public class OutboundConfigurationObject : OutboundConfigurationObjectBase
    {
        public IList<ServerObject> Vnext { get; set; } = new List<ServerObject>();
    }
}
