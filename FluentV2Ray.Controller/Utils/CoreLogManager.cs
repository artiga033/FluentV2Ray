namespace FluentV2Ray.Controller.Utils
{
    public class CoreLogManager
    {
        private readonly StreamReader StandardOutput;

        private IList<string> logs = new List<string>();
        public IList<string> Logs
        {
            get
            {
                this.ReadStdO();
                return logs;
            }
            set { logs = value; }
        }
        public CoreLogManager(StreamReader standerdOutput)
        {
            this.StandardOutput = standerdOutput;
        }
        private void ReadStdO()
        {
            while (!StandardOutput.EndOfStream)
            {
                var line = StandardOutput.ReadLine();
                if (line != null)
                    logs.Add(line);
                else break;
            }
        }
    }
}
