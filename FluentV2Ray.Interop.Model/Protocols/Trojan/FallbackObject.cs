using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Protocols.Trojan
{
    public class FallbackObject : IV2RayConfig
    {
        public string? Alpn { get; set; }
        public string? Path { get; set; }
        [DefaultValue(80)]
        public int Dest { get; set; } = 80;
        [DefaultValue(0)]
        public int Xver { get; set; }
    }
}
