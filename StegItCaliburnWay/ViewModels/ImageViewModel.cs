using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Bitmap _containerBitmapMessage;

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

        public byte[] ContainerRawMessage
        {
            get { return _containerRawMessage; }
            set
            {
                _containerRawMessage = value;
                NotifyOfPropertyChange(() => ContainerRawMessage);
            }
        }

        public Bitmap ContainerBitmapMessage
        {
            get { return _containerBitmapMessage; }
            set
            {
                _containerBitmapMessage = value;
                NotifyOfPropertyChange(() => ContainerBitmapMessage);
            }
        }

        public void OpenReadDialog()
        {
            ImageFilePicked filePicked = _filePickerDialog.OpenReadImageDialog();
            ContainerRawMessage = filePicked.Bytes;
            ContainerBitmapMessage = filePicked.Bitmap;
        }

        public void Save()
        {
            _filePickerDialog.OpenSaveDialog(DialogType.Image, HiddenMessage);
        }

        public void Hide()
        {
            HiddenMessage = _selectedImageMethod.PerformHiding(this);
        }

        public void Decode()
        {
            HiddenMessage = _selectedImageMethod.PerformDecoding(this);
        }
    }
}
