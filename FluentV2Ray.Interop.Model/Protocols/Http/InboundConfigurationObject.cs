using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Protocols.Http
{
    public class InboundConfigurationObject : InboundConfigurationObjectBase
    {
        [DefaultValue(300)]
        public int Timeout { get; set; } = 300;
        public IList<AccountObject> Accounts { get; set; } = new List<AccountObject>();
        [DefaultValue(false)]
        public bool AllowTransparent { get; set; } = false;
        public int? UserLevel { get; set; }
    }
}
