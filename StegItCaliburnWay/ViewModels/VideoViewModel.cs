using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using StegItCaliburnWay.Logic.Steganography.AudioSteganography;
using StegItCaliburnWay.Logic.Steganography.Video;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.Utils.Video;

namespace StegItCaliburnWay.ViewModels
{
    public class VideoViewModel : Screen, IStegenographyMethodViewModel
    {
        private readonly FilePickerDialog _filePickerDialog;
        private byte[] _containerRawMessage;
        private byte[] _messageToHide;
        private byte[] _hiddenMessage;
        private byte[] _hiddenRawMessage;
        private byte[] _decodedMessage;

        private VideoFile _hiddenMessageVideoFile;
        private VideoFile _containerVideoFile;

        public List<VideoMethod> VideoMethods { get; set; }
        private VideoMethod _selectedVideoMethod;

        public VideoViewModel(
            FilePickerDialog filePickerDialog,
            AviCodingMethod aviCodingMethod)
        {
            _filePickerDialog = filePickerDialog;
            VideoMethods = new List<VideoMethod>
            {
                aviCodingMethod
            };

            SelectedVideoMethod = VideoMethods[0];
        }

        public VideoMethod SelectedVideoMethod
        {
            get { return _selectedVideoMethod; }
            set
            {
                _selectedVideoMethod = value;
                NotifyOfPropertyChange(() => SelectedVideoMethod);
            }
        }

        public override string DisplayName
        {
            get { return "Wideo"; }
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

        public byte[] HiddenRawMessage
        {
            get { return _hiddenRawMessage; }
            set
            {
                _hiddenRawMessage = value;
                NotifyOfPropertyChange(() => HiddenRawMessage);
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

        public VideoFile ContainerVideoFile
        {
            get { return _containerVideoFile; }
            set
            {
                _containerVideoFile = value;
                NotifyOfPropertyChange(() => ContainerVideoFile);
            }
        }

        public byte[] DecodedMessage
        {
            get { return _decodedMessage; }
            set
            {
                _decodedMessage = value;
                NotifyOfPropertyChange(() => DecodedMessage);
            }
        }

        public void OpenReadDialog()
        {
            VideoFile file = _filePickerDialog.OpenReadVideoDialog(_selectedVideoMethod.dialogType);
            ContainerRawMessage = file.hiddenMessageBytes;
            ContainerVideoFile = file;
        }

        public Task Hide()
        {
            return Task.Factory.StartNew(() =>
            {

            });
        }



        public Task Decode()
        {
            return Task.Factory.StartNew(() =>
            {

                VideoFile file = _selectedVideoMethod.PerformDecoding(this);
                DecodedMessage = file.hiddenMessageBytes;
            });

        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public object HiddenMessageViewModel { get; private set; }
        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
