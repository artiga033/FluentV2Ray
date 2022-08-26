namespace FluentV2Ray.Interop.Model.Protocols.Vless
{
    public class UserObject : IV2RayConfig
    {
        public string Id { get; set; }
        public string Encryption { get; set; } = "none";
        public int Level { get; set; }
        public UserObject(string id)
        {
            Id = id;
        }
    }
}