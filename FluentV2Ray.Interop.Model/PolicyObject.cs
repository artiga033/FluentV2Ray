using FluentV2Ray.Interop.Model.Policy;

namespace FluentV2Ray.Interop.Model
{
    public class PolicyObject : IV2RayConfig
    {
        /// <summary>
        /// Gets or sets the polict for each user level. The keys are numbers in string form, representing user level.
        /// </summary>
        public Dictionary<string, LevelPolicyObject>? Levels { get; set; }
        public SystemPolicyObject? System { get; set; }
    }
}
