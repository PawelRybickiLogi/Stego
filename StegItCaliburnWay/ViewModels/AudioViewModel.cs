using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using StegItCaliburnWay.Logic.Steganography.AudioSteganography;
using StegItCaliburnWay.Logic.Steganography.AudioSteganography.Methods.Wave;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.Utils.Audio;

namespace StegItCaliburnWay.ViewModels
{
    public class AudioViewModel : Screen, IStegenographyMethodViewModel
    {
        private readonly FilePickerDialog _filePickerDialog;
        private byte[] _containerRawMessage;
        private byte[] _messageToHide;
        private byte[] _hiddenMessage;
        private byte[] _hiddenRawMessage;
        private byte[] _decodedMessage;
        
        private AudioFile _containerAudioFile;
        private AudioFile _hiddenAudioFile;

        private string _containerAudioInfo;
        private string _hiddenMessageAudioInfo;

        public List<AudioMethod> AudioMethods { get; set; }
        private AudioMethod _selectedAudioMethod;
        private AudioFile _hiddenMessageAudioFile;


        public AudioViewModel(FilePickerDialog filePickerDialog,
                              AudioMethod.WaveCodingMethod waveCodingMethod)
        {
            _filePickerDialog = filePickerDialog;
            AudioMethods = new List<AudioMethod>
            {
                waveCodingMethod
            };

            SelectedAudioMethod = AudioMethods[0];
        }

        public AudioMethod SelectedAudioMethod
        {
            get { return _selectedAudioMethod; }
            set
            {
                _selectedAudioMethod = value;
                NotifyOfPropertyChange(() => SelectedAudioMethod);
            }
        }

        public override string DisplayName
        {
            get { return "Dźwięk"; }
            set { }
        }

        public string ContainerAudioInfo
        {
            get { return _containerAudioInfo; }
            set
            {
                _containerAudioInfo = value;
                NotifyOfPropertyChange(() => ContainerAudioInfo);
            }
        }

        public string HiddenMessageAudioInfo
        {
            get { return _hiddenMessageAudioInfo; }
            set
            {
                _hiddenMessageAudioInfo = value;
                NotifyOfPropertyChange(() => HiddenMessageAudioInfo);
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

        public byte[] DecodedMessage
        {
            get { return _decodedMessage; }
            set
            {
                _decodedMessage = value;
                NotifyOfPropertyChange(() => DecodedMessage);
            }
        }

        public AudioFile ContainerAudioFile
        {
            get { return _containerAudioFile; }
            set
            {
                _containerAudioFile = value;
                NotifyOfPropertyChange(() => ContainerAudioFile);
            }
        }

        public AudioFile HiddenMessageAudioFile
        {
            get { return _hiddenMessageAudioFile; }
            set
            {
                _hiddenMessageAudioFile = value;
                NotifyOfPropertyChange(() => HiddenMessageAudioFile);
            }
        }

        public void OpenReadDialog()
        {
            AudioFile file = _filePickerDialog.OpenReadAudioDialog(_selectedAudioMethod.dialogType);
            ContainerRawMessage = file.bytes;
            ContainerAudioFile = file;
            ContainerAudioInfo =
                "Plik dźwiękowy kontenera obecny" + Environment.NewLine +
                "Ilość próbek: " + file.waveFile.totalSamples + Environment.NewLine +
                "Bitów na próbkę: " + file.waveFile.bitsPerSample + Environment.NewLine +
                "Ilość kanałów: " + file.waveFile.channels + Environment.NewLine +
                "Rozmiar w bajtach: " + file.bytes.Length;

        }

        public void Save()
        {
            _filePickerDialog.OpenSaveDialog(_selectedAudioMethod.dialogType, HiddenMessageAudioFile.bytes);
        }

        public object HiddenMessageViewModel { get; private set; }
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task Hide()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    AudioFile file = _selectedAudioMethod.PerformHiding(this);
                    HiddenRawMessage = file.bytes;
                    HiddenMessageAudioFile = file;
                    HiddenMessageAudioInfo =
                        "Plik dźwiękowy ukrytej wiadomości obecny" + Environment.NewLine +
                        "Ilość próbek: " + file.waveFile.totalSamples + Environment.NewLine +
                        "Bitów na próbkę: " + file.waveFile.bitsPerSample + Environment.NewLine +
                        "Ilość kanałów: " + file.waveFile.bitsPerSample + Environment.NewLine +
                        "Rozmiar w bajtach: " + file.bytes.Length;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        public Task Decode()
        {
            return Task.Factory.StartNew(() =>
            {

                AudioFile file = _selectedAudioMethod.PerformDecoding(this);
                DecodedMessage = file.hiddenMessageBytes;
            });
        }

    }
}
