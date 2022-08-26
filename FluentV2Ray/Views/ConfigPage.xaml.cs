using FluentV2Ray.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentV2Ray.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConfigPage : Page
    {
        public ConfigPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;

        }
        public ConfigViewModel ViewModel { get; set; } = App.Current.Services.GetRequiredService<ConfigViewModel>();


    }
}
