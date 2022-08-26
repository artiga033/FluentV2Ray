using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Protocols.Dokodemo_door
{
    public class InboundConfigurationObject : InboundConfigurationObjectBase
    {
        public string? Address { get; set; }
        public int? Port { get; set; }
        [DefaultValue("tcp")]
        public string? Network { get; set; }
        public int? Timeout { get; set; }
        public bool? FollowRedirect { get; set; }
        public int? UserLevel { get; set; }
    }
}
