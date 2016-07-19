using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using StegItCaliburnWay.Logic.Steganography.ImageSteganography;
using StegItCaliburnWay.Logic.TextSteganography;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.ViewModels
{
    public class ImageViewModel : Screen, IStegenographyMethodViewModel
    {
        private readonly FilePickerDialog _filePickerDialog;
        private byte[] _containerRawMessage;
        private byte[] _messageToHide;
        private byte[] _hiddenMessage;

        public List<ImageMethod> ImageMethods { get; set; }
        private ImageMethod _selectedImageMethod;

        public ImageViewModel(
            FilePickerDialog filePickerDialog,
            BitMap24 bitMap24,
            BitMap16 bitMap16,
            Gif gif)
        {
            _filePickerDialog = filePickerDialog;
            ImageMethods = new List<ImageMethod>
            {
                bitMap24,
                bitMap16,
                gif
            };

            SelectedImageMethod = ImageMethods[0];
        }

        public override string DisplayName
        {
            get { return "Obraz"; }
            set { }
        }

        public ImageMethod SelectedImageMethod
        {
            get { return _selectedImageMethod; }
            set
            {
                _selectedImageMethod = value;
                NotifyOfPropertyChange(() => SelectedImageMethod);
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

        public void OpenReadDialog()
        {
            ContainerRawMessage = _filePickerDialog.OpenReadDialog(DialogType.Image);
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
