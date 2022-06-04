using FluentV2Ray.Utils;
using FluentV2Ray.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Windows.Forms;
using WinRT.Interop;
using Windows.Graphics;
using static FluentV2Ray.Utils.Win32Api;
using FluentV2Ray.Controller;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using FluentV2Ray.Services.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentV2Ray
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuWindow : Window
    {
        private readonly II18NService i18N = App.Current.Services.GetRequiredService<II18NService>();
        public MenuWindow()
        {
            this.InitializeComponent();

            // WinUI provides few oprations on window instance. 
            // So we'll mostly use win32 API
            this.hWnd = WindowNative.GetWindowHandle(this);
            int style = Win32Api.GetWindowLong(hWnd, GWL_STYLE);
            Win32Api.SetWindowLong(hWnd, GWL_STYLE, ((style | WS_POPUP) & ~WS_CAPTION & ~WS_SIZEBOX));// hide caption, see https://stackoverflow.com/questions/2825528/removing-the-title-bar-of-external-application-using-c-sharp
                                                                                                      // also makes it unsizable, as the size will be controlled by the program itself.
            Win32Api.SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW & ~WS_EX_APPWINDOW);// set transparent 
            Win32Api.SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_HIDEWINDOW);// set topmost and hide the window, though it seems not hidden.
#if DEBUG
            Win32Api.SetLayeredWindowAttributes(hWnd, 0, 255, LWA_ALPHA);
#endif

            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            this.c_window = AppWindow.GetFromWindowId(wndId);
            c_window.TitleBar.ExtendsContentIntoTitleBar = true;
            Win32Api.ShowWindow(hWnd, SW_HIDE);

            this.menuPage.DataContext = ViewModel;
            ViewModel.NotifyIconMouseClick += HandleNotifyIconClick;
        }
        public MenuViewModel ViewModel { get; set; } = MenuViewModel.Instance;
        private AppWindow c_window;
        private IntPtr hWnd;
        private void HandleNotifyIconClick(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int x = Cursor.Position.X;
                int y = Cursor.Position.Y;

                Win32Api.SetWindowPos(this.hWnd, Win32Api.HWND_TOPMOST, x - ViewModel.HiddenWidth / 2, y - ViewModel.HiddenHeight, ViewModel.HiddenWidth, ViewModel.HiddenHeight, SWP_SHOWWINDOW);
                int extendedStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
                Win32Api.SetWindowLong(hWnd, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);// unset transparent 
                menuFlyout.ShowAt(menuPage, new Point(ViewModel.HiddenWidth / 2 - 65, ViewModel.HiddenHeight)); // dont ask why the width needs to minus 65px.From test.
                Win32Api.SetForegroundWindow(hWnd);

            }
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (WindowActivationState.Deactivated == args.WindowActivationState)
            {
                Win32Api.ShowWindow(hWnd, SW_HIDE);
            }
        }

        private void OnFlyoutClosed(object sender, object e)
        {
            Win32Api.ShowWindow(hWnd, SW_HIDE);
        }
    }
    public class EnumRadioConverter : IValueConverter
    {
        // value is the enum. parameter is the enum variant of the RadioItem
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return parameter;
        }
    }
}
