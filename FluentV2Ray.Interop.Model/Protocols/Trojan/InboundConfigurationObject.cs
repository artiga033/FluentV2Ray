namespace FluentV2Ray.Interop.Model.Protocols.Trojan
{
    public class InboundConfigurationObject : InboundConfigurationObjectBase
    {
        public IList<ClientObject> Clients { get; set; } = new List<ClientObject>();
        public IList<FallbackObject> Fallbacks { get; set; } = new List<FallbackObject>();
    }
}
