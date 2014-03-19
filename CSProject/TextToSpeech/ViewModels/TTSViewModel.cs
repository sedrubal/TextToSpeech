using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Filoe.Core.Commands;

namespace TextToSpeech.ViewModels
{
    public class TTSViewModel : Filoe.Core.PropertyChangedBase
    {
        private TextToSpeechModel _tts;
        public TextToSpeechModel TTS
        {
            get
            {
                return _tts ?? (_tts = new TextToSpeechModel());
            }
            set { SetProperty(value, ref _tts, () => TTS); }
        }

        public string Text
        {
            get
            {
                return TTS.Text;
            }
            set
            {
                TTS.Text = value;
                OnPropertyChanged(() => Text);
            }
        }
        
        public int SpeechRate
        {
            get
            {
                return TTS.Rate;
            }
            set
            {
                TTS.Rate = value;
                OnPropertyChanged(() => SpeechRate);
            }
        }

        public int SpeechVolume
        {
            get
            {
                return TTS.Volume;
            }
            set
            {
                TTS.Volume = value;
                OnPropertyChanged(() => SpeechVolume);
            }
        }

#region "Commands"

        ICommand _readCommand;
        public ICommand ReadCommand
        {
            get { return _readCommand ?? (_readCommand = new AutoDelegateCommand((o) => OnReadCommand(o), (o) => CanReadCommand(o))); }
            set { SetProperty(value, ref _readCommand, () => ReadCommand); }
        }
        public void OnReadCommand(object param)
        {
            TTS.ReadText();
        }
        public bool CanReadCommand(object param) { return !string.IsNullOrWhiteSpace(TTS.Text); }

        ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new AutoDelegateCommand((o) => OnSaveCommand(o), (o) => CanSaveCommand(o))); }
            set { SetProperty(value, ref _saveCommand, () => SaveCommand); }
        }
        public void OnSaveCommand(object param)
        {
            Microsoft.Win32.SaveFileDialog SFD = new Microsoft.Win32.SaveFileDialog();
            SFD.Filter = "Wav-Datei|*.wav";
            SFD.Title = "TextToSpeech: Text als Audio speichern";
            SFD.AddExtension = true; SFD.DefaultExt = "wav";
            if (SFD.ShowDialog() == true)
            {
                TTS.SaveTextAsAudio(Text, SFD.FileName);
            }

        }
        public bool CanSaveCommand(object param) { return !string.IsNullOrWhiteSpace(TTS.Text); }

        ICommand _stopCommand;
        public ICommand StopCommand
        {
            get { return _stopCommand ?? (_stopCommand = new AutoDelegateCommand((o) => OnStopCommand(o), (o) => CanStopCommand(o))); }
            set { SetProperty(value, ref _stopCommand, () => StopCommand); }
        }
        public void OnStopCommand(object param)
        {
            TTS.Stop();
        }
        public bool CanStopCommand(object param) { return !string.IsNullOrWhiteSpace(TTS.Text); }


        ICommand _pauseResumeCommand;
        public ICommand PauseResumeCommand
        {
            get { return _pauseResumeCommand ?? (_pauseResumeCommand = new AutoDelegateCommand((o) => OnPauseResumeCommand(o), (o) => CanPauseResumeCommand(o))); }
            set { SetProperty(value, ref _pauseResumeCommand, () => PauseResumeCommand); }
        }
        public void OnPauseResumeCommand(object param)
        {
            TTS.PauseResume();
        }
        public bool CanPauseResumeCommand(object param) { return true; }

        ICommand _optimizeTextCommand;
        public ICommand OptimizeTextCommand
        {
            get { return _optimizeTextCommand ?? (_optimizeTextCommand = new AutoDelegateCommand((o) => OnOptimizeTextCommand(o), (o) => CanOptimizeTextCommand(o))); }
            set { SetProperty(value, ref _optimizeTextCommand, () => OptimizeTextCommand); }
        }
        public void OnOptimizeTextCommand(object param)
        {
            TTS.OptimizeText();
        }
        public bool CanOptimizeTextCommand(object param) { return !string.IsNullOrWhiteSpace(TTS.Text); }



        //ICommand _setVoice;
        //public ICommand SetVoice
        //{
        //    get { return _setVoice ?? (_setVoice = new AutoDelegateCommand((o) => OnSetVoice(o), (o) => CanSetVoice(o))); }
        //    set { SetProperty(value, ref _setVoice, () => SetVoice); }
        //}
        //public void OnSetVoice(object param)
        //{
            
        //}
        //public bool CanSetVoice(object param) { return true; }
#endregion
    }
}