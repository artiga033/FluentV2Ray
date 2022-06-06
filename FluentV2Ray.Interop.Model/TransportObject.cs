using Shadowsocks.Interop.V2Ray.Transport;

namespace Shadowsocks.Interop.V2Ray
{
    public class TransportObject
    {
        private TcpObject? tcpSettings;
        private KcpObject? kcpSettings;
        private WebSocketObject? wsSettings;
        private HttpObject? httpSettings;
        private QuicObject? quicSettings;
        private DomainSocketObject? dsSettings;

        public TcpObject? TcpSettings { get => CacheGetter( tcpSettings); set => tcpSettings = value; }
        public KcpObject? KcpSettings { get => CacheGetter(kcpSettings); set => kcpSettings = value; }
        public WebSocketObject? WsSettings { get => CacheGetter(wsSettings); set => wsSettings = value; }
        public HttpObject? HttpSettings { get => CacheGetter(httpSettings); set => httpSettings = value; }
        public QuicObject? QuicSettings { get => CacheGetter(quicSettings); set => quicSettings = value; }
        public DomainSocketObject? DsSettings { get => CacheGetter(dsSettings); set => dsSettings = value; }
    }
}
