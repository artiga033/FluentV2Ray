namespace FluentV2Ray.Interop.Model.Inbound
{
    public class AllocateObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the port allocation strategy.
        /// Available values: "always" | "random"
        /// </summary>
        public string Strategy { get; set; } = "always";

        /// <summary>
        /// Gets or sets the random port refreshing interval in minutes.
        /// Defaults to 5 minutes.
        /// </summary>
        public int Refresh { get; set; } = 5;

        /// <summary>
        /// Gets or sets the number of random ports.
        /// Defaults to 3.
        /// </summary>
        public int? Concurrency { get; set; } = 3;
    }
}
