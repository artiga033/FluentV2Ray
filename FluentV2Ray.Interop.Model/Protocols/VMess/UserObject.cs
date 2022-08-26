using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Protocols.VMess
{
    public class UserObject
    {
        public string Id { get; set; }
        [DefaultValue(0)]
        public int AlterId { get; set; }
        [DefaultValue("auto")]
        public string Security { get; set; } = "auto";
        public int Level { get; set; }
        public UserObject(string id)
        {
            Id = id;
        }
    }
}
