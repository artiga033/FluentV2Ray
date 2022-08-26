namespace FluentV2Ray.Interop.Model.Protocols.Http
{
    public class AccountObject : IV2RayConfig
    {
        public string User { get; set; }
        public string Pass { get; set; }

        public AccountObject(string user, string pass)
        {
            User = user;
            Pass = pass;
        }
    }
}
