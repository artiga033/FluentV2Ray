namespace FluentV2Ray.Interop.Model.Protocols.Trojan
{
    public class ClientObject : IV2RayConfig
    {
        public string Password { get; set; }
        public string? Email { get; set; }
        public int Level { get; set; }

        public ClientObject(string password)
        {
            Password = password;
        }
    }
}
