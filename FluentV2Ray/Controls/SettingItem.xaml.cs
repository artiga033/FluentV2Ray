using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentV2Ray.Controls
{
    [ContentProperty(Name = nameof(MainContent))]
    public sealed partial class SettingItem : UserControl
    {
        public SettingItem()
        {
            this.InitializeComponent();
        }


        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(SettingItem), new PropertyMetadata(null));


        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(SettingItem), new PropertyMetadata(null));



        public object MainContent
        {
            get { return (object)GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainContentProperty =
            DependencyProperty.Register("MainContent", typeof(object), typeof(SettingItem), new PropertyMetadata(null));


    }
}
