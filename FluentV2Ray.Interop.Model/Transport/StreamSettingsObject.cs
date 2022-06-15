namespace Shadowsocks.Interop.V2Ray.Transport
{
    public class StreamSettingsObject : TransportObject
    {
        private SockoptObject? sockopt;
        private TlsObject? tlsSettings;

        /// <summary>
        /// Gets or sets the transport protocol type.
        /// Defaults to "tcp".
        /// Available values: "tcp" | "kcp" | "ws" | "http" | "domainsocket" | "quic"
        /// </summary>
        public string? Network { get; set; }

        /// <summary>
        /// Gets or sets the transport encryption type.
        /// Defaults to "none" (no encryption).
        /// Available values: "none" | "tls"
        /// </summary>
        public string? Security { get; set; }

        public TlsObject? TlsSettings { get => CacheGetter(tlsSettings); set => tlsSettings = value; }
        public SockoptObject? Sockopt { get => CacheGetter(sockopt); set => sockopt = value; }

        public static StreamSettingsObject DefaultWsTls() => new()
        {
            Network = "ws",
            Security = "tls",
            TlsSettings = new(),
        };
        public static StreamSettingsObject DefaultWsTlsAllInit() => new()
        {
            Network = "ws",
            Security = "tls",
            TlsSettings = new(),
            WsSettings = new(),
            DsSettings = new(),
            HttpSettings = new(),
            KcpSettings = new(),
            QuicSettings = new(),
            TcpSettings = new(),
        };
    }
}
