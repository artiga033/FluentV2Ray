using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Protocols.Blackhole
{
    public class ResponseObject
    {
        /// <summary>
        /// Gets or sets the reponding type.
        /// Available values; "none" | "http"
        /// </summary>
        [DefaultValue("none")]
        public string Type { get; set; } = "none";
    }
}