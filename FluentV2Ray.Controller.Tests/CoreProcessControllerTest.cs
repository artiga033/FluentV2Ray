using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
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
            CoreProcessController con = new CoreProcessController(new CoreConfigController(NullLogger<CoreConfigController>.Instance));
            string result = con.CheckVersion();
            output.WriteLine(result);
            Assert.Contains("V2Ray ", result);
        }
        [Fact]
        public void Start_Stop_Successful()
        {
            CoreProcessController con = new CoreProcessController(new CoreConfigController(NullLogger<CoreConfigController>.Instance));
            con.ConfigPath = "Assets/plainConfig.json";
            con.Start();
            Assert.True(con.IsRunning);
            Assert.NotEmpty(con.Logs);
            string stdout = string.Join('\n', con.Logs);
            output.WriteLine(stdout);
            Assert.Contains("Assets/plainConfig.json", stdout);

            con.Stop();
            Assert.False(con.IsRunning);
        }
        [Theory]
        [InlineData("Assets/plainConfig.json", true)]
        [InlineData("Assets/invalidConfig.json", false)]
        [InlineData("Assets/errorConfig.json", false)]
        public void CheckConfig_Correct(string filepath, bool expected)
        {
            CoreProcessController con = new CoreProcessController(new CoreConfigController(NullLogger<CoreConfigController>.Instance));
            con.ConfigPath = filepath;
            Assert.Equal(expected, con.CheckConfigValid());
        }
    }
}