using CommunityToolkit.Mvvm.ComponentModel;
using FluentV2Ray.Controller;
using FluentV2Ray.Interop.Model.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentV2Ray.Interop.Model;
using FluentV2Ray.Models.Exceptions;

namespace FluentV2Ray.ViewModels
{
    public class SettingsPageViewModel : ObservableObject
    {
        private readonly CoreConfigController _config;

        public SettingsPageViewModel(CoreConfigController config)
        {
            _config = config;
            var inbounds = _config.Config.Inbounds;
            this.SocksInbound = inbounds.FirstOrDefault(x => x.Protocol == Protocol.Socks) ?? throw new CoreConfigException();
            this.HttpInbound = inbounds.FirstOrDefault(x => x.Protocol == Protocol.Http) ?? throw new CoreConfigException();
        }
        public void SaveConfig() => _config.Save();
        public InboundObject SocksInbound { get; set; }
        public InboundObject HttpInbound { get; set; }
    }
}
