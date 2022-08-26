using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FluentV2Ray.Interop.Model.JsonHelpers
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Returns the string formatted with the options's <see cref="JsonSerializerOptions.PropertyNamingPolicy"/> naming policy.
        /// If no policy is specified, returns the string itself.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string PolicizeNaming(this JsonSerializerOptions options, string s) => options.PropertyNamingPolicy?.ConvertName(s) ?? s;
    }
}
