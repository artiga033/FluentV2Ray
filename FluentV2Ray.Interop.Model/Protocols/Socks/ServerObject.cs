namespace FluentV2Ray.Interop.Model.Protocols.Socks
{
    public class ServerObject : IV2RayConfig
    {

        public string Address { get; set; }
        public int Port { get; set; }
        public IList<UserObject>? Users { get; set; } = new List<UserObject>();
        public ServerObject(string address, int port)
        {
            Address = address;
            Port = port;
        }
    }
}
