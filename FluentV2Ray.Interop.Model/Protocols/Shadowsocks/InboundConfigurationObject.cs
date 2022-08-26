namespace FluentV2Ray.Interop.Model.Protocols.Shadowsocks
{
    public class InboundConfigurationObject : InboundConfigurationObjectBase
    {
        public string? Email { get; set; }

        public string Method { get; set; }

        public string Password { get; set; }

        public int Level { get; set; } = 0;

        public string Network { get; set; } = "tcp";
        public InboundConfigurationObject(string method, string password)
        {
            Method = method;
            Password = password;
        }
    }
}
