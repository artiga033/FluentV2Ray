using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentV2Ray.Interop.Model
{
    /// <summary>
    /// Connection Observatory confirms the outbounds' status through periodic connection.
    /// </summary>
    public class ObservatoryObject
    {
        public ICollection<string>? SubjectSelector { get; set; }
        public string? ProbeURL;
        public string? ProbeInterval;
    }
}
