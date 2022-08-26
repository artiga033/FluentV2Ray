namespace FluentV2Ray.Interop.Model.Protocols.Vless
{
    public class FallbackObject : IV2RayConfig
    {
        public string Alpn { get; set; } = "";
        public string Path { get; set; } = "";
        public string Dest { get; set; }
        public int Xver { get; set; } = 1;
        public FallbackObject(string dest)
        {
            Dest = dest;
        }
    }
}