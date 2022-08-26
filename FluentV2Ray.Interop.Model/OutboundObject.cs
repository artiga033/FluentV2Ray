using FluentV2Ray.Interop.Model.JsonHelpers;
using FluentV2Ray.Interop.Model.Outbound;
using FluentV2Ray.Interop.Model.Protocols;
using FluentV2Ray.Interop.Model.Transport;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FluentV2Ray.Interop.Model
{
    [JsonConverter(typeof(OutboundObjectJsonConverter))]
    public class OutboundObject : IV2RayConfig, INotifyPropertyChanged // TODO >> Find another way, we are not going (and supposed) to implement this interface for v2ray config models.
    {
        private string tag;
        public string Tag { get => tag; set { tag = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tag))); } }
        [DefaultValue("0.0.0.0")]
        public string? SendThrough { get; set; }
        public Protocol Protocol { get; set; }
        public OutboundConfigurationObjectBase? Settings { get; set; }
        public StreamSettingsObject? StreamSettings { get; set; }
        [DefaultValue(null)]
        public ProxySettingsObject? ProxySettings { get; set; }
        [DefaultValue(null)]
        public MuxObject? Mux { get; set; }

        public OutboundObject()
        {
            this.tag = "";
            Protocol = Protocol.Freedom;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets the <see cref="OutboundObject"/> for the SOCKS server.
        /// </summary>
        /// <param name="name">SOCKS server name. Used as outbound tag.</param>
        /// <param name="socksEndPoint">The SOCKS server.</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static OutboundObject GetSocks(string name, string ipAddress, int port) => new()
        {
            Tag = name,
            Protocol = Protocol.Socks,
            Settings = new Protocols.Socks.OutboundConfigurationObject(ipAddress, port),
        };

        /// <summary>
        /// Gets the <see cref="OutboundObject"/> for the Shadowsocks server.
        /// Plugins are not supported.
        /// </summary>
        /// <param name="method">The encryption method</param>
        /// <returns></returns>
        public static OutboundObject GetShadowsocks(string name, string host, int port, string method, string password)
        {
            //if (!string.IsNullOrEmpty(server.Plugin))
            //    throw new InvalidOperationException("V2Ray doesn't support SIP003 plugins.");

            return new()
            {
                Tag = name,
                Protocol = Protocol.Shadowsocks,
                Settings = new Protocols.Shadowsocks.OutboundConfigurationObject(host, port, method, password),
            };
        }

        /// <summary>
        /// Gets the <see cref="OutboundObject"/> for the Trojan server.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static OutboundObject GetTrojan(string name, string address, int port, string password) => new()
        {
            Tag = name,
            Protocol = Protocol.Trojan,
            Settings = new Protocols.Trojan.OutboundConfigurationObject(address, port, password),
        };

        /// <summary>
        /// Gets the <see cref="OutboundObject"/> for the VMess server.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OutboundObject GetVMess(string name, string address, int port, string id) => new()
        {
            Tag = name,
            Protocol = Protocol.Vmess,
            Settings = new Protocols.VMess.OutboundConfigurationObject(address, port, id),
        };
        /// <summary>
        /// Get the <see cref="OutboundObject"/> for the freedom outbound.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static OutboundObject GetFreedom(string name) => new()
        {
            Tag = name,
            Protocol = Protocol.Freedom,
            Settings = new Protocols.Freedom.OutboundConfigurationObject(),
        };
    }
}
