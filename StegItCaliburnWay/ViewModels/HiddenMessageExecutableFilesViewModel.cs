using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    class HiddenMessageExecutableFilesViewModel : Screen
    {
        private byte[] _hiddenMessageText;

        public string HiddenMessage
        {
            get 
            { 
                return "Plik wykonywalne z ukrytą wiadomością  obecny" + Environment.NewLine +
                       "Rozmiar w bajtach: " + _hiddenMessageText.Length;
            }
        }

        public HiddenMessageExecutableFilesViewModel(byte[] hiddenMessageText)
        {
            _hiddenMessageText = hiddenMessageText;
        }

        public void Clear()
        {
            _hiddenMessageText = null;
            NotifyOfPropertyChange(() => HiddenMessage);
        }
    }

}
