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

        public string ExecutablePath { get; set; } = "v2ray.exe";
        public string ConfigPath { get; set; } = "config.json";
        public bool IsRunning = false;
        public StreamReader? StandardOutput { get => p?.StandardOutput; }
        private Process? p = null;
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
        public string CheckVersion()
        {
            var p = new Process();
            p.StartInfo.FileName = ExecutablePath;
            p.StartInfo.Arguments = "-version";
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            return p.StandardOutput.ReadLine() ?? "";
        }

    }
}
