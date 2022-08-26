using FluentV2Ray.Extensions.Markup;
using FluentV2Ray.Services.Interfaces;
using Microsoft.Windows.ApplicationModel.Resources;

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
                var raw = resourceLoader.GetString(key);
                if (!string.IsNullOrWhiteSpace(raw))
                    locale = raw;
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

        public string GetLocale(LocaleKey key)
        {
            return GetLocale(key.ToString());
        }
    }
}
