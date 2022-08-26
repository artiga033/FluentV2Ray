namespace FluentV2Ray.Interop.Model.Protocols.Trojan
{
    public class ServerObject : IV2RayConfig
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public int Level { get; set; }

        public ServerObject(string address, int port, string password)
        {
            Address = address;
            Port = port;
            Password = password;
        }
    }
}
