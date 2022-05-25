using FluentV2Ray.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Markup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Key { get; set; }
        public string After { get; set; } = "";
        public string Before { get; set; } = "";

        protected override object ProvideValue()
        {
            return Before + _i18N.GetLocale(Key) + After;
        }
    }
}
