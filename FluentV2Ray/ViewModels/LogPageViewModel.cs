using FluentV2Ray.Controller;
using System.Collections.ObjectModel;

namespace FluentV2Ray.ViewModels
{
    public class LogPageViewModel
    {
        private readonly CoreProcessController _pCon;
        public ObservableCollection<string> CoreLogSource => _pCon.Logs;
        public LogPageViewModel(CoreProcessController pCon)
        {
            this._pCon = pCon;
        }
    }
}
