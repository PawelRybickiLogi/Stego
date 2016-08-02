using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using StegItCaliburnWay.Utils.Video;

namespace StegItCaliburnWay.ViewModels
{
    class HiddenMessageVideoViewModel : Screen
    {
        private VideoFile _hiddenMessageVideoFile;

        public string HiddenMessage
        {
            get
            {
                return "Plik dźwiękowy kontenera obecny" + Environment.NewLine +
                       "Ilość próbek: " + _hiddenMessageVideoFile + Environment.NewLine +
                       "Bitów na próbkę: " + _hiddenMessageVideoFile + Environment.NewLine +
                       "Ilość kanałów: " + _hiddenMessageVideoFile + Environment.NewLine +
                       "Rozmiar w bajtach: " + _hiddenMessageVideoFile;
            }
        }

        public HiddenMessageVideoViewModel(VideoFile hiddenMessageVideoFile)
        {
            _hiddenMessageVideoFile = hiddenMessageVideoFile;
        }

        public void Clear()
        {
            _hiddenMessageVideoFile = null;

            NotifyOfPropertyChange(() => HiddenMessage);
        }
    }
}
