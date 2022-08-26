namespace FluentV2Ray.Interop.Model.Protocols.Http
{
    public class ServerObject : IV2RayConfig
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public IList<AccountObject> Users { get; set; } = new List<AccountObject>();
        public ServerObject(string address, int port)
        {
            this.Address = address;
            this.Port = port;
        }
    }
}
