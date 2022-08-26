using System.ComponentModel;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;

namespace FluentV2Ray.Interop.Model.Transport
{
    public class SockoptObject
    {
        [DefaultValue(0)]
        public int Mark { get; set; }
        public bool? TcpFastOpen { get; set; }

        /// <summary>
        /// Currenly only works on linux.
        /// </summary>
        [Obsolete("Not support windows yet.")]
        [UnsupportedOSPlatform("Windows")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? Tproxy { get; set; }
    }
}
