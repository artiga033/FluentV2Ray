namespace FluentV2Ray.Interop.Model.Protocols.Socks
{
    public class UserObject
    {
        public string User { get; set; }
        public string Pass { get; set; }
        public int Level { get; set; }
        public UserObject(string user, string pass, int level)
        {
            User = user;
            Pass = pass;
            Level = level;
        }
    }
}