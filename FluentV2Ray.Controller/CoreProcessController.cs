using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentV2Ray.Controller
{
    public class CoreProcessController
    {
        private readonly CoreConfigController _configCon;
        public string ExecutablePath { get; set; } = "v2ray.exe";
        public string ConfigPath { get; set; } = "coreConfig.json";

        public bool IsRunning = false;
        public StreamReader? StandardOutput { get => p?.StandardOutput; }
        private Process? p = null;

        public CoreProcessController(CoreConfigController configCon)
        {
            this._configCon = configCon;
            this.ConfigPath = configCon.ConfigPath;
        }
        public void Start()
        {
            if (IsRunning)
                return;
            if (p != null)
                this.Stop();

            p = new Process();
            p.StartInfo.FileName = ExecutablePath;
            p.StartInfo.Arguments = $"-c {ConfigPath}";
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            this.IsRunning = true;
        }
        public void Stop()
        {
            p.Kill();
            p.Dispose();
            p = null;
            this.IsRunning = false;
        }
        public void Restart()
        {
            if (IsRunning)
                Stop();
            Start();
        }
        public string CheckVersion()
        {
            var p = new Process();
            p.StartInfo.FileName = ExecutablePath;
            p.StartInfo.Arguments = "-version";
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            return p.StandardOutput.ReadLine() ?? "";
        }
        public bool CheckConfigValid()
        {
            if (!File.Exists(ConfigPath))
            {
                throw new FileNotFoundException();
            }
            var p = new Process();
            p.StartInfo.FileName = ExecutablePath;
            p.StartInfo.Arguments = "-test -config " + ConfigPath;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            string? s = p.StandardOutput.ReadLine();

            while (s != null && !s.Contains("Reading config:"))
                s = p.StandardOutput.ReadLine();
            s = p.StandardOutput.ReadLine();
            return s?.Contains("Configuration OK") ?? false;
        }
    }
    public static partial class ControllerDIExtensions
    {
        public static IServiceCollection AddCoreProcessController(this IServiceCollection services)
        {
            services.AddSingleton<CoreProcessController>();
            return services;
        }
    }
}
