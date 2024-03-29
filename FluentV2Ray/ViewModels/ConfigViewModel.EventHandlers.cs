﻿using Microsoft.UI.Xaml.Controls;

namespace FluentV2Ray.ViewModels
{
    public partial class ConfigViewModel
    {
        public void TransportSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsHttpTrans));
            OnPropertyChanged(nameof(IsTcp));
            OnPropertyChanged(nameof(IsKcp));
            OnPropertyChanged(nameof(IsDomainSocket));
            OnPropertyChanged(nameof(IsWs));
            OnPropertyChanged(nameof(IsQuic));
            OnPropertyChanged(nameof(StreamSettings));
        }
        public void TlsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsTls));
            OnPropertyChanged(nameof(StreamSettings));
        }
    }
}
