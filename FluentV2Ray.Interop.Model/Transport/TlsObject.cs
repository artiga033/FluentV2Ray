using System.ComponentModel;
namespace FluentV2Ray.Interop.Model.Transport
{
    public class TlsObject : IV2RayConfig
    {
        public string? ServerName { get; set; }
        [DefaultValue(false)]
        public bool AllowInsecure { get; set; }
        [DefaultValue(new[] { "h2", "http/1.1" })]
        public IList<string>? Alpn { get; set; }
        public IList<CertificateObject>? Certificates { get; set; }
        public bool DisableSystemRoot { get; set; }

    }
}
