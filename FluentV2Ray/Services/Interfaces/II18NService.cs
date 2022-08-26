namespace FluentV2Ray.Services.Interfaces
{
    public interface II18NService
    {
        public string GetLocale(string key);
        public string GetLocale(Extensions.Markup.LocaleKey key);
    }
}
