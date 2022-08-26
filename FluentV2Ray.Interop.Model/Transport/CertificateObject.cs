using System.ComponentModel;

namespace FluentV2Ray.Interop.Model.Transport
{
    public class CertificateObject
    {
        [DefaultValue("encipherment")]
        public string Usage { get; set; } = "encipherment";
        public string? CertificateFile { get; set; }
        public string? KeyFile { get; set; }
        public IEnumerable<string>? Certificate { get; set; }
        public IEnumerable<string>? Key { get; set; }

        public CertificateObject() { }
        public CertificateObject(IEnumerable<string> certificate, IEnumerable<string> key)
        {
            this.Certificate = certificate;
            this.Key = key;
        }
        public static CertificateObject DefaultFromFile => new()
        {
            CertificateFile = "",
            KeyFile = "",
        };
    }
}
