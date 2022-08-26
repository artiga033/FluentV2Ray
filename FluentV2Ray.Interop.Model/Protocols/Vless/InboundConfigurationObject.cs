namespace FluentV2Ray.Interop.Model.Protocols.Vless
{
    public class InboundConfigurationObject : InboundConfigurationObjectBase
    {
        public IList<ClientObject> Clients { get; set; } = new List<ClientObject>();
        public string Decryption { get; set; } = "none";
        public IList<FallbackObject> Fallbacks { get; set; } = new List<FallbackObject>();
    }
}
