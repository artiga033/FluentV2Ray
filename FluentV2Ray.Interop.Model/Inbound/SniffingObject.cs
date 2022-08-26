namespace FluentV2Ray.Interop.Model.Inbound
{
    public class SniffingObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets whether to enable sniffing.
        /// Defaults to true (enabled).
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the list of protocols that destination override is enabled.
        /// </summary>
        public IList<string> DestOverride { get; set; } = new List<string>() { "http", "tls" };

        /// <summary>
        /// Gets or sets whether the target address is sniffed
        /// solely based on metadata.
        /// Defaults to false.
        /// Change to true to use FakeDNS.
        /// </summary>
        public bool MetadataOnly { get; set; }
    }
}
