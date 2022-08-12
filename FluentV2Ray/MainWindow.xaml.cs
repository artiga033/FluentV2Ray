using FluentV2Ray.Utils;
using FluentV2Ray.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentV2Ray
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private MainWindow()
        {
            this.InitializeComponent();
            this.Closed += (_, _) => Instance = null;
        }
        private readonly IList<(string Tag, Type Page)> _pages = new[]
        {
            ("config",typeof(ConfigPage)),
            ("log",typeof(LogPage))
        };
        private static MainWindow? Instance;
        /// <summary>
        /// Navigate to the page and show up as foreground window
        /// </summary>
        public static void Page(Type type)
        {
            if (Instance == null)
                Instance = new MainWindow();
            Instance.rootFrame.Navigate(type);
            Instance.Activate();
            Win32Api.SetForegroundWindow(WindowNative.GetWindowHandle(Instance));
        }
        public static void Page<T>() where T : Page
        {
            Page(typeof(T));
        }
        private void OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                this.rootFrame.Navigate(_pages.First(x => x.Tag == (string)args.SelectedItemContainer.Tag).Page);
            }
        }
    }
}
