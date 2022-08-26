namespace FluentV2Ray.Interop.Model.Protocols.VMess
{
    public class ServerObject
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public IList<UserObject> Users { get; set; } = new List<UserObject>();
        public ServerObject() { Address = ""; }
        public ServerObject(string address, int port, string id)
        {
            Address = address;
            Port = port;
            Users = new List<UserObject>() { new UserObject(id) };
        }
    }
}
