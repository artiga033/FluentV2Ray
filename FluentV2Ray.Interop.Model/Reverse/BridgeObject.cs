namespace FluentV2Ray.Interop.Model.Reverse
{
    public class BridgeObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the inbound tag for the bridge.
        /// </summary>
        public string Tag { get; set; } = "";

        /// <summary>
        /// Gets or sets the domain name for the bridge.
        /// Can be omitted.
        /// </summary>
        public string? Domain { get; set; }

        public BridgeObject(string tag)
        {
            this.Tag = tag;
        }
        public BridgeObject(string tag, string domain)
        {
            this.Tag = tag;
            this.Domain = domain;
        }
    }
}
