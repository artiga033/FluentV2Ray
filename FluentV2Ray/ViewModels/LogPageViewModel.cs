using FluentV2Ray.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
