namespace Shadowsocks.Interop.V2Ray
{
    public class LogObject
    {
        /// <summary>
        /// Gets or sets the path to the access log file.
        /// Defaults to empty, which prints to stdout.
        /// V2Ray 4.20 added a special value none, which means no access to log.
        /// </summary>
        public string Access { get; set; }

        /// <summary>
        /// Gets or sets the path to the error log file.
        /// Defaults to empty, which prints to stdout.
        /// V2Ray 4.20 added a special value none, which means no access to error.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// Defaults to warning.
        /// Available values: "debug" | "info" | "warning" | "error" | "none"
        /// </summary>
        public string Loglevel { get; set; }

        public LogObject()
        {
            Access = "";
            Error = "";
            Loglevel = "warning";
        }
    }
}
