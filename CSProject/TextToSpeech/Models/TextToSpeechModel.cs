using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.IO;

namespace TextToSpeech
{
    public class TextToSpeechModel : Filoe.Core.PropertyChangedBase
    {
        private string _text;
        public string Text
        {
            get
            {
                return _text ?? (_text = "");
            }
            set
            {
                SetProperty<string>(value, ref _text, () => Text);
            }
        }

        public TextToSpeechModel()
        {
              // Configure the audio output. 
              Synth.SetOutputToDefaultAudioDevice();

              // Speak a string.
              Synth.SpeakAsync("Willkommen");

              Synth.SpeakCompleted += new EventHandler<System.Speech.Synthesis.SpeakCompletedEventArgs>(ReadFinnish);

              //string t = "";
              //foreach (string v in InstalledVoices)
              //{
              //    t += Environment.NewLine + v;
              //}
              //Text = t;
        }
        
        private SpeechSynthesizer _synth;
        public SpeechSynthesizer Synth
        {
            get
            {
                return _synth ??
                    (_synth = new System.Speech.Synthesis.SpeechSynthesizer());
            }
            set
            {
                SetProperty<SpeechSynthesizer>(value, ref _synth, () => Synth);
            }
        }

        public void OptimizeText()
        {
            //this.Text = this.Text.Replace("\r\n\r\n", ". ");
            this.Text = this.Text.Replace(Environment.NewLine, " ");
        }

        public int Rate
        {
            get
            {
                return Synth.Rate;
            }
            set
            {
                Synth.Rate = value;
                OnPropertyChanged(() => Rate);
            }
        }

        public int Volume
        {
            get
            {
                return Synth.Volume;
            }
            set
            {
                Synth.Volume = value;
                OnPropertyChanged(() => Volume);
            }
        }

        public void ReadText()
        {
            if (Synth.State == SynthesizerState.Ready)
            {
                // Configure the audio output. 
                Synth.SetOutputToDefaultAudioDevice();
                Synth.SpeakAsync(Text);
            }
        }

        public void SaveTextAsAudio(string text, string file)
        {
            Synth.SetOutputToWaveFile(file);
            Synth.SpeakAsync(text);
        }

        public void Stop()
        {
            Synth.SpeakAsyncCancelAll();
        }

        public void PauseResume()
        {
            if (Synth.State == SynthesizerState.Speaking)
                Synth.Pause();
            else if (Synth.State == SynthesizerState.Paused)
                Synth.Resume();
        }

        private void ReadFinnish(System.Object sender, System.Speech.Synthesis.SpeakCompletedEventArgs e)
        {
            Synth.SetOutputToNull();
        }

        #region voice

        public string CurrentVoice
        {
            get
            {
                return Synth.Voice.Name;
            }
            set
            {
                Synth.SelectVoice(value);
                OnPropertyChanged(() => CurrentVoice);
            }
        }

        public List<string> InstalledVoices
        {
            get
            {
                List<string> l = new List<string>();
                foreach (InstalledVoice v in Synth.GetInstalledVoices())
                    l.Add(v.VoiceInfo.Name);
                return l;
            }
        }

        #endregion
    }
}