using System.ComponentModel;

namespace FluentV2Ray.Interop.Model
{
    public class LogObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the path to the access log file.
        /// Defaults to empty, which prints to stdout.
        /// V2Ray 4.20 added a special value none, which means no access to log.
        /// </summary>
        [DefaultValue("")]
        public string? Access { get; set; }

        /// <summary>
        /// Gets or sets the path to the error log file.
        /// Defaults to empty, which prints to stdout.
        /// V2Ray 4.20 added a special value none, which means no access to error.
        /// </summary>
        [DefaultValue("")]
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// Available values: "debug" | "info" | "warning" | "error" | "none"
        /// </summary>
        [DefaultValue("warning")]
        public string Loglevel { get; set; } = "warning";
    }
}
