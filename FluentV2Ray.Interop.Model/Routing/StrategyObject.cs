using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Routing
{
    public class StrategyObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the type of balance.
        /// Available values: "random" | "leastPing" (v4.38.0+)
        /// </summary>
        [DefaultValue("random")]
        public string Type { get; set; } = "random";
    }
}