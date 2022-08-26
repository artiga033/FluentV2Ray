using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentV2Ray.Controls
{
    public sealed partial class DictConfigEditor : UserControl
    {
        public DictConfigEditor()
        {
            this.InitializeComponent();
        }
        public IDictionary<string, string>? Dict { get; set; }

        private void headerKeyBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void headerValueBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            string key = textbox.PlaceholderText; // this is hard-coded. don't ask why. unless there's a better approach
            this.Dict[key] = textbox.Text;
        }
    }
    //public class KeyValuePairValueModifyConverter : IValueConverter
    //{
    //    // This converts KeyValuePair value to string
    //    public object Convert(object value, Type targetType, object parameter, string language)
    //    {
    //        return ((KeyValuePair<string, string>)value).Value;
    //    }
    //    // This is called when ui string changed
    //    // value is ui string
    //    // parameter is the dictionary
    //    public object ConvertBack(object value, Type targetType, object parameter, string language)
    //    {

    //        throw new NotImplementedException();
    //    }
    //}
}
