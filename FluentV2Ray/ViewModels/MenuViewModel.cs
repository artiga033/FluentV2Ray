using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentV2Ray.Controller;
using FluentV2Ray.Models;
using FluentV2Ray.Services;
using FluentV2Ray.Services.Interfaces;
using FluentV2Ray.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
namespace FluentV2Ray.ViewModels
{
    public partial class MenuViewModel : ObservableObject
    {
        private readonly CoreProcessController _processController = App.Current.Services.GetRequiredService<CoreProcessController>();
        private readonly IAppSettingService _setting;
        private static MenuViewModel unique = new MenuViewModel();
        public static MenuViewModel Instance => unique;
        private MenuViewModel()
        {
            notifyIcon.Icon = Properties.Resources.icon;
            notifyIcon.Visible = true;
            notifyIcon.MouseClick += (s, e) => NotifyIconMouseClick?.Invoke(this, e);

            this._setting = App.Current.Services.GetRequiredService<IAppSettingService>();
            this.appSetting = _setting.AppSetting;

            this.ExitCommand = new(() =>
            {
                _processController.Stop();
                Application.Exit();
                Environment.Exit(0);
            });
            this.RunningModeEnabledCommand = new(() =>
            {
                appSetting.RunningMode = RunningMode.Enabled;
                _setting.SaveAppSetting();
                _processController.Restart();
            });
            this.RunningModeDisabledCommand = new(() =>
            {
                appSetting.RunningMode = RunningMode.Disabled;
                _setting.SaveAppSetting();
                _processController.Stop();
            });
        }
        private System.Windows.Forms.NotifyIcon notifyIcon = new();

        public event MouseEventHandler? NotifyIconMouseClick;
        private int hiddenWidth = 500;
        private int hiddenHeight = 700;
        private AppSetting appSetting;
        public int HiddenWidth { get => hiddenWidth; set => SetProperty(ref hiddenWidth, value); }
        public int HiddenHeight { get => hiddenHeight; set => SetProperty(ref hiddenHeight, value); }
        #region RunningModeProperties
        public bool RunningModeEnabled => appSetting.RunningMode == RunningMode.Enabled;
        public bool RunningModeDisabled => appSetting.RunningMode == RunningMode.Disabled;
        #endregion
        public RelayCommand RunningModeEnabledCommand { get; }
        public RelayCommand RunningModeDisabledCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand ConfigureCommand { get; } = new RelayCommand(() => MainWindow.Page<ConfigPage>());
    }
}
