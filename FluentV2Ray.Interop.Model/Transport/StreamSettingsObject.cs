using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Transport
{
    public class StreamSettingsObject : TransportObject
    {
        /// <summary>
        /// Gets or sets the transport protocol type.
        /// Defaults to "tcp".
        /// Available values: "tcp" | "kcp" | "ws" | "http" | "domainsocket" | "quic"
        /// </summary>
        [DefaultValue("tcp")]
        public string? Network { get; set; }

        /// <summary>
        /// Gets or sets the transport encryption type.
        /// Defaults to "none" (no encryption).
        /// Available values: "none" | "tls"
        /// </summary>
        [DefaultValue("none")]
        public string? Security { get; set; }

        public TlsObject? TlsSettings { get; set; }
        public SockoptObject? Sockopt { get; set; }

        public static StreamSettingsObject DefaultAllInit() => DefaultAllInit("tcp", "none");

        /// <param name="network">Available values: "tcp" | "kcp" | "ws" | "http" | "domainsocket" | "quic"</param>
        /// <param name="security">Available values: "none" | "tls"</param>
        /// <returns></returns>
        public static StreamSettingsObject DefaultAllInit(string network, string security) => new()
        {
            Network = network,
            Security = security,
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
