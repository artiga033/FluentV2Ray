namespace FluentV2Ray.Interop.Model.Protocols.VMess
{
    public class ClientObject : IV2RayConfig
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int Level { get; set; }
        public int AlterId { get; set; }
        public ClientObject(string id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}