using FluentV2Ray.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentV2Ray.Extensions.Markup
{
    public sealed class LocaleExtension : MarkupExtension
    {
        private readonly II18NService _i18N;

        public LocaleExtension()
        {
            this._i18N = App.Current.Services.GetRequiredService<II18NService>();
        }
        /// <summary>
        /// Key for the localed text
        /// </summary>
        public string Key { get; set; }

        protected override object ProvideValue()
        {
            return _i18N.GetLocale(Key);
        }
    }
}
