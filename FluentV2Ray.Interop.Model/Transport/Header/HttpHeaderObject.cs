using FluentV2Ray.Interop.Model.Transport.Header.Http;

namespace FluentV2Ray.Interop.Model.Transport.Header
{
    public class HttpHeaderObject : HeaderObject
    {
        public HttpRequestObject request { get; set; }

        public HttpResponseObject response { get; set; }

        public HttpHeaderObject()
        {
            Type = "http";
            request = new();
            response = new();
        }
    }
}
