using System.ComponentModel;

namespace FluentV2Ray.Interop.Model
{
    public class ApiObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the outbound tag for the API.
        /// </summary>
        [DefaultValue("")]
        public string Tag { get; set; } = "";

        /// <summary>
        /// Gets or sets the list of API services to enable.
        /// </summary>
        [DefaultValue(new string[] { })]
        public IList<string> Services { get; set; } = new List<string>();

        /// <summary>
        /// Gets the default API object.
        /// </summary>
        public static ApiObject Default => new()
        {
            Tag = "api",
            Services = new List<string>()
            {
                "HandlerService",
                "LoggerService",
                "StatsService",
            },
        };
    }
}
