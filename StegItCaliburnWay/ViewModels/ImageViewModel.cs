using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    public class ImageViewModel : Screen, IStegenographyMethodViewModel
    {
        private byte[] _containerRawMessage;
        private byte[] _messageToHide;
        private byte[] _hiddenMessage;

        public override string DisplayName
        {
            get { return "Obraz"; }
            set { }
        }

        public byte[] MessageToHide
        {
            get { return _messageToHide; }
            set
            {
                _messageToHide = value;
                NotifyOfPropertyChange(() => MessageToHide);
            }
        }

        public byte[] HiddenMessage
        {
            get { return _hiddenMessage; }
            set
            {
                _hiddenMessage = value;
                NotifyOfPropertyChange(() => HiddenMessage);
            }
        }

        public byte[] ContainerRawMessage
        {
            get { return _containerRawMessage; }
            set
            {
                _containerRawMessage = value;
                NotifyOfPropertyChange(() => ContainerRawMessage);
            }
        }

        public void Hide()
        {

        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void SaveToFile()
        {
            throw new NotImplementedException();
        }

        public void Decode()
        {
            throw new NotImplementedException();
        }
    }
}
