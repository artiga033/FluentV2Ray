using FluentV2Ray.Controller.Utils;
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
        public StreamReader? StandardOutput => p?.StandardOutput;
        public event DataReceivedEventHandler? OutputDataReceived;
        public event DataReceivedEventHandler? ErrorDataReceived;
        private Process? p = null;
        private Job coreProcessJob = new Job();
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
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.OutputDataReceived += (s, e) => { this.OutputDataReceived?.Invoke(s, e); };
            p.ErrorDataReceived += (s, e) => { this.ErrorDataReceived?.Invoke(s, e); };
            p.Exited += this.OnProcessExited;
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            coreProcessJob.AddProcess(p.Handle);
            this.IsRunning = true;
        }
        public void Stop()
        {
            if (IsRunning)
            {
                this.IsRunning = false;
                p.Kill();
                p.Dispose();
                p = null;
            }
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
        private void OnProcessExited(object? sender, EventArgs e)
        {
            if (IsRunning)
            {
                this.IsRunning = false;
                throw new CoreUnexpectedExitedException();
            }
        }
        class CoreUnexpectedExitedException : Exception { }
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
