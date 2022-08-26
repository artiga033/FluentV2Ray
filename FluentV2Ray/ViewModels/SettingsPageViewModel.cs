using CommunityToolkit.Mvvm.ComponentModel;
using FluentV2Ray.Controller;
using Shadowsocks.Interop.V2Ray;
using FluentV2Ray.Interop.Model.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentV2Ray.ViewModels
{
    public class SettingsPageViewModel : ObservableObject
    {
        private readonly CoreConfigController _config;

        public SettingsPageViewModel(CoreConfigController config)
        {
            _config = config;
            var inbounds = _config.Config.Inbounds;
        }
    }
}
