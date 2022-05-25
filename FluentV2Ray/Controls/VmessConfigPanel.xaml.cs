using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Shadowsocks.Interop.V2Ray.Protocols.VMess;
using Shadowsocks.Interop.V2Ray;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentV2Ray.Controls
{
    public sealed partial class VmessConfigPanel : UserControl
    {
        public OutboundObject OutboundObject { get; set; }
        private OutboundConfigurationObject Settings { get; set; }
        public VmessConfigPanel()
        {
            
        }
        //public VmessConfigPanel(OutboundObject outboundObject)
        //{
        //    this.InitializeComponent();
        //    if (outboundObject.Settings is not OutboundConfigurationObject)
        //        throw new ArgumentException("Specified config type is not a vmess config.");
        //    OutboundObject = outboundObject;
        //    Settings = (OutboundConfigurationObject)OutboundObject.Settings;
        //}
    }
}
