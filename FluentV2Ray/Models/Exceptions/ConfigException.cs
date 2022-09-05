using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentV2Ray.Models.Exceptions
{
    public abstract class ConfigException : Exception { }
    public class CoreConfigException : ConfigException { }
    public class AppConfigException : ConfigException { }
}
