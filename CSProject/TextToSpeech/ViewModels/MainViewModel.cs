using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        TTSViewModel _ttsviewmodel;
        public TTSViewModel TTSViewModel
        {
            get { return _ttsviewmodel ?? (_ttsviewmodel = new TTSViewModel()); }
            set { SetProperty(value, ref _ttsviewmodel, () => TTSViewModel); }
        }

    }
}
