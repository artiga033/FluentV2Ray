namespace FluentV2Ray.Interop.Model.Protocols.Dns
{
    public class OutboundConfigurationObject : OutboundConfigurationObjectBase
    {
        public string? Network { get; set; }
        public string? Address { get; set; }
        public int? Port { get; set; }
    }
}
