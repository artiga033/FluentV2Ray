namespace FluentV2Ray.Models
{
    public class AppSetting
    {
        public RunningMode RunningMode { get; set; } = RunningMode.Disabled;
    }
    public enum RunningMode
    {
        Disabled,
        Enabled,
        SysProxy
    }
}
