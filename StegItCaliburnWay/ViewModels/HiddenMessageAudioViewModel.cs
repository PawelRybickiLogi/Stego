using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using StegItCaliburnWay.Logic.Steganography.AudioSteganography.Methods.Wave;

namespace StegItCaliburnWay.ViewModels
{
    class HiddenMessageAudioViewModel : Screen
    {
        private AudioFile _hiddenMessageAudioFile;

        public string HiddenMessage
        {
            get
            {
                return "Plik dźwiękowy kontenera obecny" + Environment.NewLine +
                       "Ilość próbek: " + _hiddenMessageAudioFile.waveFile.totalSamples + Environment.NewLine +
                       "Bitów na próbkę: " + _hiddenMessageAudioFile.waveFile.bitsPerSample + Environment.NewLine +
                       "Ilość kanałów: " + _hiddenMessageAudioFile.waveFile.channels + Environment.NewLine +
                       "Rozmiar w bajtach: " + _hiddenMessageAudioFile.bytes.Length;
            }
        }

        public HiddenMessageAudioViewModel(AudioFile hiddenMessageAudioFile)
        {
            _hiddenMessageAudioFile = hiddenMessageAudioFile;
        }

        public void Clear()
        {
            _hiddenMessageAudioFile = null;

            NotifyOfPropertyChange(() => HiddenMessage);
        }
    }
}
