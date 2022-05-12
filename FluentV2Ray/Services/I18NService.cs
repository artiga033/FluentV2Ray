using FluentV2Ray.Services.Interfaces;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentV2Ray.Services
{
    internal class I18NService : II18NService
    {
        private readonly ResourceLoader resourceLoader;

        public I18NService()
        {
            string culture = System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag;
            try
            {
                resourceLoader = new ResourceLoader(ResourceLoader.GetDefaultResourceFilePath(), culture);
            }
            catch
            {
                resourceLoader = new ResourceLoader(ResourceLoader.GetDefaultResourceFilePath(), "en-US");
            }
        }
        public string GetLocale(string key)
        {
            string locale = "%" + key + "%";
            try
            {
                locale = resourceLoader.GetString(key); ;
            }
            catch
            {
                try
                {
                    locale = new ResourceLoader(ResourceLoader.GetDefaultResourceFilePath(), "en-US").GetString(key);
                }
                catch { }
            }
            return locale;
        }
    }
}
