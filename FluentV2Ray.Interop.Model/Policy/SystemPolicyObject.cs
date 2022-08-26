namespace FluentV2Ray.Interop.Model.Policy
{
    public class SystemPolicyObject : IV2RayConfig
    {
        public bool StatsInboundUplink { get; set; } = false;
        public bool StatsInboundDownlink { get; set; } = false;
        public bool StatsOutboundUplink { get; set; } = false;
        public bool StatsOutboundDownlink { get; set; } = false;
    }
}
