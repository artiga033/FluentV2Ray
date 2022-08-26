using FluentV2Ray.Interop.Model.Routing;
using System.ComponentModel;

namespace FluentV2Ray.Interop.Model;

public class RoutingObject : IV2RayConfig
{
    /// <summary>
    /// Gets or sets the domain strategy used for routing.
    /// Available values: "AsIs" | "IPIfNonMatch" | "IPOnDemand"
    /// </summary>
    [DefaultValue("AsIs")]
    public string DomainStrategy { get; set; } = "AsIs";

    /// <summary>
    /// Gets or sets the domain matcher used for routing.
    /// Available values: "linear" | "mph"
    /// </summary>
    [DefaultValue("linear")]
    public string DomainMatcher { get; set; } = "linear";

    /// <summary>
    /// Gets or sets the list of routing rules.
    /// </summary>
    public IList<RuleObject>? Rules { get; set; }

    /// <summary>
    /// Gets or sets the list of load balancers.
    /// </summary>
    public IList<BalancerObject>? Balancers { get; set; }
}
