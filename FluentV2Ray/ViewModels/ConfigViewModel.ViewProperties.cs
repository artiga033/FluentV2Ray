using FluentV2Ray.Interop.Model.Protocols;
using FluentV2Ray.Interop.Model.Transport;
using P = FluentV2Ray.Interop.Model.Protocols;

namespace FluentV2Ray.ViewModels
{
    public partial class ConfigViewModel
    {
        public bool IsVmess => selectedItem?.Protocol == Protocol.Vmess;
        public P.VMess.OutboundConfigurationObject? VmessOutboundSettings { get => selectedItem?.Settings as P.VMess.OutboundConfigurationObject; }
        public bool IsShadowsocks => selectedItem?.Protocol == Protocol.Shadowsocks;
        public P.Shadowsocks.OutboundConfigurationObject? SSOutboundSettings { get => selectedItem?.Settings as P.Shadowsocks.OutboundConfigurationObject; }
        public bool IsHttp => selectedItem?.Protocol == Protocol.Http;
        public bool IsVless => selectedItem?.Protocol == Protocol.Vless;

        public bool IsWs => selectedItem?.StreamSettings?.Network == "ws";
        public bool IsTcp => selectedItem?.StreamSettings?.Network == "tcp";
        public bool IsKcp => selectedItem?.StreamSettings?.Network == "kcp";
        public bool IsHttpTrans => selectedItem?.StreamSettings?.Network == "http";
        public bool IsDomainSocket => selectedItem?.StreamSettings?.Network == "domainsocket";
        public bool IsQuic => selectedItem?.StreamSettings?.Network == "quic";
        public StreamSettingsObject? StreamSettings
        {
            get
            {
                if (SelectedItem is null) return null;
                else
                {
                    if (SelectedItem.StreamSettings is null)
                        SelectedItem.StreamSettings = new StreamSettingsObject() { };
                }
                return SelectedItem.StreamSettings;
            }
        }

        public bool IsTls => selectedItem?.StreamSettings?.Security == "tls";
    }
}
