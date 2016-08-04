using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
                return 
                    "Plik wideo ukrytej wiadomości obecny" + Environment.NewLine +
                    "Lokalizacja pliku: " + _hiddenMessageVideoFile.FileName + Environment.NewLine +
                    "Wysokość obrazu: " + _hiddenMessageVideoFile.FrameHeight + Environment.NewLine +
                    "Szerokość obrazu: " + _hiddenMessageVideoFile.FrameWidth + Environment.NewLine +
                    "Ilość klatek: " + _hiddenMessageVideoFile.FrameCount + Environment.NewLine +
                    "Częstotliwość klatek/s: " + _hiddenMessageVideoFile.FrameRate;
            }
        }

        public HiddenMessageVideoViewModel(
            VideoFile hiddenMessageVideoFile)
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
