using Xunit;
using FluentV2Ray.Controller;
using Xunit.Abstractions;

namespace FluentV2Ray.Controller.Tests
{
    public class CoreProcessControllerTest
    {
        private readonly ITestOutputHelper output;
        public CoreProcessControllerTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void CheckVersion_Correct()
        {
            CoreProcessController con = new CoreProcessController();
            string result = con.CheckVersion();
            output.WriteLine(result);
            Assert.Contains("V2Ray ", result);
        }
        [Fact]
        public void Start_Stop_Successful()
        {
            CoreProcessController con = new CoreProcessController();
            con.ConfigPath = "plainConfig.json";
            con.Start();
            Assert.True(con.IsRunning);
            Assert.NotNull(con.StandardOutput);
            string stdout = con.StandardOutput.ReadToEnd();
            output.WriteLine(stdout);
            Assert.Contains("plainConfig.json", stdout);

            con.Stop();
            Assert.False(con.IsRunning);
            Assert.Null(con.StandardOutput);
        }
    }
}