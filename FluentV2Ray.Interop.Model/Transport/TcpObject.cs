using FluentV2Ray.Interop.Model.Transport.Header;

namespace FluentV2Ray.Interop.Model.Transport
{
    public class TcpObject
    {
        /// <summary>
        /// Gets or sets whether to use PROXY protocol.
        /// </summary>
        public bool AcceptProxyProtocol { get; set; }

        /// <summary>
        /// Gets or sets the header options.
        /// </summary>
        public object Header { get; set; }

        public TcpObject()
        {
            AcceptProxyProtocol = false;
            Header = new HeaderObject();
        }

        public static TcpObject DefaultHttp => new()
        {
            Header = new HttpHeaderObject(),
        };
    }
}
