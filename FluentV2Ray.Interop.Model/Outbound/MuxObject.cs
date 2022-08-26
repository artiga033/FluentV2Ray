using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Outbound
{
    public class MuxObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets whether to enable mux.
        /// Defaults to false (disabled).
        /// </summary>
        [DefaultValue(false)]
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Gets or sets the concurrency for a single TCP connection when using mux.
        /// Defaults to 8.
        /// Range: [1, 1024].
        /// Set to -1 to disable the mux module.
        /// </summary>
        [DefaultValue(8)]
        public int Concurrency { get; set; } = 8;
    }
}
