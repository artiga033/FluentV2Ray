using FluentV2Ray.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Markup;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        public LocaleKey Key { get; set; } = LocaleKey.Undefined;
        public LocaleKey? After { get; set; } = null;
        public LocaleKey? Before { get; set; } = null;
        public string Seprator { get; set; } = "";

        protected override object ProvideValue()
        {
            StringBuilder builder = new();
            if (Before.HasValue)
                builder.Append(_i18N.GetLocale(Before.Value)).Append(Seprator);
            builder.Append(_i18N.GetLocale(Key));
            if (After.HasValue)
                builder.Append(Seprator).Append(_i18N.GetLocale(After.Value));
            return builder.ToString();
        }
    }
}
