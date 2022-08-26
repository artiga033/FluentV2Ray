namespace FluentV2Ray.Interop.Model.Protocols.Shadowsocks
{
    public class ServerObject : IV2RayConfig
    {
        public string? Email { get; set; }

        public string Address { get; set; }

        public int Port { get; set; }

        public string Method { get; set; }

        public string Password { get; set; }

        public int? Level { get; set; }
        public bool IvCheck { get; set; }

        public ServerObject()
        {
            Address = "";
            Method = "";
            Password = "";
        }
        public ServerObject(string address, int port, string method, string password)
        {
            Address = address;
            Port = port;
            Method = method;
            Password = password;
        }
    }
}
