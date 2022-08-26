namespace FluentV2Ray.Interop.Model.Routing
{
    public class BalancerObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the outbound tag for the load balancer.
        /// </summary>
        public string Tag { get; set; } = "";

        /// <summary>
        /// Gets or sets a list of outbound tags
        /// to include in the load balancer.
        /// This uses prefix match.
        /// </summary>
        public List<string> Selector { get; set; } = new List<string>();
        public StrategyObject? Strategy { get; set; }
    }
}
