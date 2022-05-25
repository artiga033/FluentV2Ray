using FluentV2Ray.Interop.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FluentV2Ray.Controller.Tests
{
    public class CoreConfigControllerTest
    {
        private readonly ITestOutputHelper _output;

        public CoreConfigControllerTest(ITestOutputHelper output)
        {
            this._output = output;
        }
        [Fact]
        public void LoadDefaultConfig_Successful()
        {
            CoreConfigController controller = new CoreConfigController(NullLogger<CoreConfigController>.Instance);
            controller.Load();
        }
        [Fact]
        public void SaveConfig_Successful()
        {
            var filepath = "testconf.json";
            CoreConfigController controller = new CoreConfigController(NullLogger<CoreConfigController>.Instance, filepath);

            controller.Load();
            controller.Save();

            Assert.True(File.Exists(filepath));
            Assert.False(string.IsNullOrEmpty(File.ReadAllText(filepath)));
        }
        [Fact]
        public void DefaultConfig_Valid()
        {
            var filepath = "default.json";
            CoreConfigController confCon = new CoreConfigController(NullLogger<CoreConfigController>.Instance, filepath);

            var defaultConf = Config.Default;
            var conf = confCon.Config;
            var properties = typeof(Config).GetProperties();
            foreach (var property in properties.Where(p => p.CanWrite))
            {
                var value = property.GetValue(defaultConf);
                property.SetValue(conf, value);
            }

            confCon.Save();

            Assert.True(File.Exists(filepath));
            _output.WriteLine(File.ReadAllText(filepath));
            CoreProcessController procCon = new CoreProcessController(confCon);
            Assert.True(procCon.CheckConfigValid());
        }
    }
}
