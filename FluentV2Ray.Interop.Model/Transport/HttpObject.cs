namespace FluentV2Ray.Interop.Model.Transport
{
    public class HttpObject
    {
        public List<string> Host { get; set; }
        public string Path { get; set; }

        public HttpObject()
        {
            Host = new() { "" };
            Path = "/";
        }
    }
}
