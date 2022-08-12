using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentV2Ray.Controls
{
    public sealed partial class LogViewer : UserControl
    {
        private ObservableCollection<string> logSource = new();

        public LogViewer()
        {
            this.InitializeComponent();
            this.LogSource.CollectionChanged += OnLogUpdated;
        }

        private void OnLogUpdated(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems! != null)
            {
                // DISPATCHER
                this.DispatcherQueue.TryEnqueue(()=>this.logbox.Text = String.Join("\n", e.NewItems.OfType<string>()));
            }
        }

        public ObservableCollection<string> LogSource
        {
            get => logSource; set
            {
                logSource = value;
                this.logbox.Text = String.Join("\n", logSource);
                logSource.CollectionChanged += OnLogUpdated;
            }
        }
    }
}
