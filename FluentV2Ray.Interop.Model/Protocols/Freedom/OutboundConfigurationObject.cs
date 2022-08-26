using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Protocols.Freedom
{
    public class OutboundConfigurationObject : OutboundConfigurationObjectBase
    {
        /// <summary>
        /// Gets or sets the domain strategy
        /// Available values: "AsIs" | "UseIP" | "UseIPv4" | "UseIPv6"
        /// </summary>
        [DefaultValue("AsIs")]
        public string DomainStrategy { get; set; } = "AsIs";
        public string? Redirect { get; set; }
        public int? UserLevel { get; set; }
    }
}
