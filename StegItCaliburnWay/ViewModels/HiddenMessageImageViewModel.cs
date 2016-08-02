using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    class HiddenMessageImageViewModel : Screen
    {
        private byte[] _hiddenMessageImage;

        public byte[] HiddenMessage
        {
            get { return _hiddenMessageImage; }
        }

        public HiddenMessageImageViewModel(byte[] hiddenMessageImage)
        {
            _hiddenMessageImage = hiddenMessageImage;
        }

        public void Clear()
        {
            _hiddenMessageImage = null;

            NotifyOfPropertyChange(() => HiddenMessage);
        }
    }
}
