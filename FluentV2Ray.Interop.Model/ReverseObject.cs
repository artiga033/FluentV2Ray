using FluentV2Ray.Interop.Model.Reverse;

namespace FluentV2Ray.Interop.Model
{
    public class ReverseObject : IV2RayConfig
    {
        public IList<BridgeObject> Bridges { get; set; } = new List<BridgeObject>();
        public IList<PortalObject> Portals { get; set; } = new List<PortalObject>();
    }
}
