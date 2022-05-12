﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace FluentV2Ray.ViewModels
{
    public partial class MenuViewModel : ObservableObject
    {
        private static MenuViewModel unique = new MenuViewModel();
        public static MenuViewModel Instance => unique;
        private MenuViewModel()
        {
            notifyIcon.Icon = Properties.Resources.icon;
            notifyIcon.Visible = true;
            notifyIcon.MouseClick += (s, e) => NotifyIconMouseClick?.Invoke(this, e);
        }
        private System.Windows.Forms.NotifyIcon notifyIcon = new();

        public event MouseEventHandler? NotifyIconMouseClick;
        private int hiddenWidth = 500;
        private int hiddenHeight = 700;
        public int HiddenWidth { get => hiddenWidth; set => SetProperty(ref hiddenWidth, value); }
        public int HiddenHeight { get => hiddenHeight; set => SetProperty(ref hiddenHeight, value); }
    }
}