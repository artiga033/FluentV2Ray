namespace FluentV2Ray.Interop.Model
{
    public class Config : IV2RayConfig
    {
        public LogObject? Log { get; set; }
        public ApiObject? Api { get; set; }
        public DnsObject? Dns { get; set; }
        public RoutingObject? Routing { get; set; }
        public PolicyObject? Policy { get; set; }
        // Use IList<T> to enable random access, which improves performance with WinUI's ListView Control.
        // As a library we do not concern using ObservableCollection. These work should be done when the App startup.
        public IList<InboundObject> Inbounds { get; set; } = new List<InboundObject>();
        public IList<OutboundObject> Outbounds { get; set; } = new List<OutboundObject>();
        public TransportObject? Transport { get; set; }
        public StatsObject? Stats { get; set; }
        public ReverseObject? Reverse { get; set; }
        public IList<FakeDnsObject>? FakeDns { get; set; }
        public BrowserForwarderObject? BrowserForwarder { get; set; }
        public ObservatoryObject? Observatory { get; set; }
        public static Config Default => new()
        {
            Api = ApiObject.Default,
            Inbounds = new[] { InboundObject.DefaultLocalSocks, InboundObject.DefaultLocalHttp },
            Outbounds = Array.Empty<OutboundObject>(),
            Log = new LogObject() { Loglevel = "info" },
            Routing = new()
        };
    }
}
