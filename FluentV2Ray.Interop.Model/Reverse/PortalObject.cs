using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Reverse
{
    public class PortalObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the outbound tag for the portal.
        /// </summary>
        [DefaultValue("")]
        public string Tag { get; set; } = "";

        /// <summary>
        /// Gets or sets the domain name for the portal.
        /// </summary>
        [DefaultValue("")]
        public string Domain { get; set; } = "";

    }
}
