namespace FluentV2Ray.Interop.Model.Protocols.Socks
{
    public class AccountObject
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