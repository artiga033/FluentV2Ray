namespace FluentV2Ray.Interop.Model.Protocols.Vless
{
    public class ClientObject : IV2RayConfig
    {
        public string Id { get; set; }
        public int Level { get; set; }
        public string? Email { get; set; }
        public ClientObject(string id)
        {
            Id = id;
        }
    }
}