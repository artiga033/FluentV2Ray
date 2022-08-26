using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentV2Ray.Controller;
using FluentV2Ray.Interop.Model;
using FluentV2Ray.Interop.Model.Protocols;
using FluentV2Ray.Interop.Model.Transport;
using System;
using System.Collections.ObjectModel;
using P = FluentV2Ray.Interop.Model.Protocols;

namespace FluentV2Ray.ViewModels
{
    public partial class ConfigViewModel : ObservableObject
    {
        private readonly CoreConfigController _configController;
        private readonly CoreProcessController _processController;
        private OutboundObject? selectedItem;

        public ObservableCollection<OutboundObject> Outbounds { get; }
        public OutboundObject? SelectedItem
        {
            get => selectedItem; set
            {
                SetProperty(ref selectedItem, value);
                OnPropertyChanged(nameof(IsHttp));
                OnPropertyChanged(nameof(IsShadowsocks));
                OnPropertyChanged(nameof(IsVless));
                OnPropertyChanged(nameof(IsVmess));
                OnPropertyChanged(nameof(VmessOutboundSettings));
                OnPropertyChanged(nameof(SSOutboundSettings));
                OnPropertyChanged(nameof(StreamSettings));
                OnPropertyChanged(nameof(IsDomainSocket));
                OnPropertyChanged(nameof(IsHttpTrans));
                OnPropertyChanged(nameof(IsTcp));
                OnPropertyChanged(nameof(IsKcp));
                OnPropertyChanged(nameof(IsWs));
                OnPropertyChanged(nameof(IsQuic));
                TransportSelectionChanged(this, null);
                TlsSelectionChanged(this, null);
            }
        }

        public ConfigViewModel(CoreConfigController configController, CoreProcessController processController)
        {
            this._configController = configController;
            this._processController = processController;

            // If the IList Object is not ObservableCollection, the listview object can't respond to collection change;
            // So we'll make it an ObservableCollection, copying the original list.
            if (_configController.Config.Outbounds is not ObservableCollection<OutboundObject>)
                _configController.Config.Outbounds = new ObservableCollection<OutboundObject>(_configController.Config.Outbounds);
            this.Outbounds = (ObservableCollection<OutboundObject>)_configController.Config.Outbounds;

            #region Initialize Commands
            this.AddCommand = new(Add);
            this.DeleteCommand = new(Delete);
            this.ApplyCommand = new(Apply);
            #endregion
        }
        public RelayCommand<Protocol> AddCommand { get; }
        public RelayCommand<OutboundObject> DeleteCommand { get; }
        public RelayCommand ApplyCommand { get; }
        public void Add(Protocol protocol)
        {
            Outbounds.Add(protocol switch
            {
                Protocol.Vmess => new OutboundObject() { Tag = "New Vmess Config", Protocol = Protocol.Vmess, Settings = new P.VMess.OutboundConfigurationObject("", 0, ""), StreamSettings = StreamSettingsObject.DefaultAllInit() },
                Protocol.Shadowsocks => new OutboundObject() { Tag = "New Shadowsocks Config", Protocol = Protocol.Shadowsocks, Settings = new P.Shadowsocks.OutboundConfigurationObject("", 0, "", ""), StreamSettings = StreamSettingsObject.DefaultAllInit() },
                Protocol.Http => new OutboundObject() { Tag = "New Http Config", Protocol = Protocol.Http, Settings = new P.Http.OutboundConfigurationObject("", 0) },
                _ => throw new NotImplementedException()
            });
        }
        public void Delete(OutboundObject? target)
        {
            if (target != null)
            {
                this.Outbounds.Remove(target);
            }
        }
        public void Apply()
        {
            _configController.Save();
            _processController.Restart();
        }

        // TODO: From here, it seems we'd better make a interface for all the outbound settings model class.
        private T? TryConvertOutboutSetting<T>(OutboundObject outboundObject) where T : OutboundConfigurationObjectBase
        {
            if (outboundObject.Settings == null)
                outboundObject.Settings = Activator.CreateInstance<T>(); ;
            return outboundObject.Settings as T;
        }
    }
}
