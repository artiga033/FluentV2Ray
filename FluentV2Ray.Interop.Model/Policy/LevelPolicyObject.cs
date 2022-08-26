using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Policy
{
    public class LevelPolicyObject : IV2RayConfig
    {
        [DefaultValue(4)]
        public int? Handshake { get; set; } = 4;
        [DefaultValue(300)]
        public int? ConnIdle { get; set; } = 300;
        [DefaultValue(2)]
        public int? UplinkOnly { get; set; } = 2;
        [DefaultValue(5)]
        public int? DownlinkOnly { get; set; } = 5;
        [DefaultValue(false)]
        public bool? StatsUserUplink { get; set; } = false;
        [DefaultValue(false)]
        public bool? StatsUserDownlink { get; set; } = false;
        [DefaultValue(512)]
        public int? BufferSize { get; set; } = 512;
    }
}
