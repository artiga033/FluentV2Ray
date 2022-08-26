using FluentV2Ray.Models;

namespace FluentV2Ray.Services.Interfaces
{
    public interface IAppSettingService
    {
        public AppSetting AppSetting { get; }
        public bool CheckSettingValid();
        public void SaveAppSetting();
        public string SettingPath { get; }
    }
}
