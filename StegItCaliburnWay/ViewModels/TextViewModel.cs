using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using StegIt.Text;
using StegItCaliburnWay.Logic.TextSteganography;

namespace StegItCaliburnWay.ViewModels
{
   

    public class TextViewModel : Screen, IStegenographyMethodViewModel
    {
        private byte[] _containerRawMessage;
        private byte[] _messageToHide;
        private byte[] _hiddenMessage;

        public TextViewModel(
            SemanticCodingMethod semanticCodingMethod,
            WhiteSpaceCodingMethod whiteSpaceCodingMethod)
        {
            TextMethods = new List<TextMethod>
            {
                semanticCodingMethod,
                whiteSpaceCodingMethod
            };

            SelectedTextMethod = TextMethods[0];
        }

        public override string DisplayName
        {
            get { return "Tekst"; }
            set { }
        }

        public List<TextMethod> TextMethods { get; set; } 

        private TextMethod _selectedTextMethod;

        public TextMethod SelectedTextMethod
        {
            get { return _selectedTextMethod; }
            set
            {
                _selectedTextMethod = value;
                NotifyOfPropertyChange(() => SelectedTextMethod);
            }
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
            HiddenMessage = _selectedTextMethod.PerformHiding(this);
        }

        public void Decode()
        {
            HiddenMessage = _selectedTextMethod.PerformDecoding(this);
        }

        public void SaveToFile()
        {
            throw new NotImplementedException();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }

        public void TextMethodChanged(SelectionChangedEventArgs eventArgs)
        {

        }
    }
}
