using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FluentV2Ray.Interop.Model.Routing
{
    public class RuleObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the domain match algorithm.
        /// Available values: "linear" | "mph"
        /// settings here is prior to <see cref="RoutingObject.DomainMatcher"/>
        /// </summary>
        public string? DomainMatcher { get; set; } = "linear";
        /// <summary>
        /// Current support "field" only.
        /// </summary>
        [DefaultValue("field")]
        public string Type { get; set; } = "field";
        public List<string>? Domains { get; set; }
        /// <summary>
        /// Gets or sets the target IP to match
        /// <para>
        /// check <see href="https://www.v2fly.org/config/routing.html#ruleobject"/> for detail.
        /// </para>
        /// </summary>
        [JsonPropertyName("ip")]
        public List<string>? IP { get; set; }
        /// <summary>
        /// Gets or sets the port number or range.
        /// can be a number from (0,65536)
        /// or a string like a-b, represents a range of [a,b]
        /// (4.18+) a string of both forms above, seperated by `,` e.g 53,443,1000-2000
        /// </summary>
        public object? Port { get; set; }
        /// <summary>
        /// <para>Same as <see cref="Port"/></para>
        /// <inheritdoc cref="Port"/>
        /// </summary>
        public object? SourcePort { get; set; }
        /// <summary>
        /// Gets or sets the connection method to match.
        /// Available values: "tcp" | "udp" | "tcp,udp"
        /// </summary>
        public string? Network { get; set; }
        /// <summary>
        /// Gets or sets the source IP to match
        /// <para>
        /// check <see href="https://www.v2fly.org/config/routing.html#ruleobject"/> for detail.
        /// </para>
        /// </summary>
        public List<string>? Source { get; set; }
        /// <summary>
        /// Gets or sets the users to match
        /// </summary>
        public List<string>? User { get; set; }
        /// <summary>
        /// Gets or sets the Inbound tag to match
        /// </summary>
        public List<string>? InboundTag { get; set; }
        /// <summary>
        /// Gets or sets the Protocol to match.
        /// Available elements: ["http" | "tls" | "bittorrent"]
        /// </summary>
        public List<string>? Protocol { get; set; }
        public string? Attrs { get; set; }
        /// <summary>
        /// Gets or sets the Outbound tag to match
        /// </summary>
        public string? OutboundTag { get; set; }
        /// <summary>
        /// Gets or sets the balancer tag to match
        /// </summary>
        public string? BalancerTag { get; set; }
    }
}
