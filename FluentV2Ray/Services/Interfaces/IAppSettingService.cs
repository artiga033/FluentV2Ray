using FluentV2Ray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
