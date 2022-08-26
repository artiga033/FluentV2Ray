using FluentV2Ray.Interop.Model.Transport;

namespace FluentV2Ray.Interop.Model
{
    public class TransportObject : IV2RayConfig
    {
        public TcpObject? TcpSettings { get; set; }
        public KcpObject? KcpSettings { get; set; }
        public WebSocketObject? WsSettings { get; set; }
        public HttpObject? HttpSettings { get; set; }
        public QuicObject? QuicSettings { get; set; }
        public DomainSocketObject? DsSettings { get; set; }
    }
}
