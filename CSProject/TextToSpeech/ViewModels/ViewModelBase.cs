using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech.ViewModels
{
    public class ViewModelBase : Filoe.Core.PropertyChangedBase
    {
        //du hast keine viewmodel base, willste aber auf main zugreifen.... wie soll das gehen? -> genau -> gar nicht
        static MainViewModel _instance;
        public static MainViewModel Instance
        {
            get { return _instance ?? (_instance = new MainViewModel()); }
            set { _instance = value; }
        }

        public MainViewModel Main
        {
            get { return Instance; }
        }
    }
}

