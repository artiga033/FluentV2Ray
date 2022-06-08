using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using FluentV2Ray.Services.Interfaces;
using FluentV2Ray.Services;
using FluentV2Ray.Controller;
using System.Diagnostics.CodeAnalysis;
using FluentV2Ray.ViewModels;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentV2Ray
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Services = ConfigureServices();
        }
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MenuWindow();
            //m_window.Activate();
            var setting = Services.GetRequiredService<IAppSettingService>().AppSetting;
            var processCon = Services.GetRequiredService<CoreProcessController>();

            if (setting.RunningMode == Models.RunningMode.Enabled)
            {
                processCon.Start();
            }
        }

        private Window? m_window;

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // services
            services.AddSingleton<II18NService, I18NService>()
                .AddSingleton<IAppSettingService, AppSettingService>();
            // controllers
            services.AddLogging()
                .AddCoreConfigController()
                .AddCoreProcessController();
            // viewmodels
            services.AddTransient<ConfigViewModel>();
            return services.BuildServiceProvider();
        }
    }
}
