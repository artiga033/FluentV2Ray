
namespace FluentV2Ray.Interop.Model.Protocols.Vless
{
    public class ServerObject : IV2RayConfig
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public IList<UserObject> Users { get; set; } = new List<UserObject>();
        public ServerObject(string address, int port, string id)
        {
            Address = address;
            Port = port;
            Users = new List<UserObject>() { new(id) };
        }
    }
}