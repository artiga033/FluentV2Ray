namespace FluentV2Ray.Interop.Model.V5
{
    public class Config
    {
        public LogObject? Log { get; set; }
        public DnsObject? Dns { get; set; }
        public RoutingObject? Routing { get; set; }
        public IList<InboundObject> Inbounds { get; set; } = new List<InboundObject>();
        public IList<OutboundObject> Outbounds { get; set; } = new List<OutboundObject>();
        public ServiceObject? Services { get; set; }

    }
}
