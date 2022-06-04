using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentV2Ray.Models
{
    public class AppSetting
    {
        public RunningMode RunningMode { get; set; } = RunningMode.Disabled;
    }
    public enum RunningMode
    {
        Disabled,
        Enabled
    }
}
