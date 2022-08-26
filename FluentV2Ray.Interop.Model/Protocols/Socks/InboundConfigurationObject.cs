using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Protocols.Socks
{
    public class InboundConfigurationObject : InboundConfigurationObjectBase
    {
        /// <summary>
        /// Available values: "noauth" | "password"
        /// </summary>
        [DefaultValue("noauth")]
        public string? Auth { get; set; } = "noauth";
        public List<AccountObject>? Accounts { get; set; }
        public bool? Udp { get; set; }
        public string Ip { get; set; } = "";
        public int? UserLevel { get; set; }
        public InboundConfigurationObject() { }
    }
}
