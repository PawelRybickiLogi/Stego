using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using StegItCaliburnWay.Logic.Steganography.ExecutableFiles;
using StegItCaliburnWay.Logic.Steganography.ExecutableFiles.Methods;

namespace StegItCaliburnWay.ViewModels
{
    public class ExecutableFilesViewModel : Screen, IStegenographyMethodViewModel
    {
        private readonly FilePickerDialog _filePickerDialog;

        private byte[] _messageToHide;
        private byte[] _containerRawMessage;
        private byte[] _decodedMessage;

        public object HiddenMessageViewModel { get; private set; }

        public List<ExecutableFilesMethod> ExecutableFilesMethods { get; set; }
        private ExecutableFilesMethod _selectedExecutableFilesMethod;
        private byte[] _hiddenRawMessage;

        public ExecutableFilesViewModel(
            FilePickerDialog filePickerDialog,
            ExecutableFilesMethod.EndOfFile endOfFile)
        {
            _filePickerDialog = filePickerDialog;
            ExecutableFilesMethods = new List<ExecutableFilesMethod>
            {
                endOfFile
            };

            SelectedExecutableFilesMethod = ExecutableFilesMethods[0];
        }

        public override string DisplayName
        {
            get { return "Pliki wykonywalne"; }
            set { }
        }

        public ExecutableFilesMethod SelectedExecutableFilesMethod
        {
            get { return _selectedExecutableFilesMethod; }
            set
            {
                _selectedExecutableFilesMethod = value;
                NotifyOfPropertyChange(() => SelectedExecutableFilesMethod);
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

        public byte[] MessageToHide
        {
            get { return _messageToHide; }
            set
            {
                _messageToHide = value;
                NotifyOfPropertyChange(() => MessageToHide);
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

        public async Task Hide()
        {
            HiddenRawMessage = await PerformHiding();
            HiddenMessageViewModel = new HiddenMessageExecutableFilesViewModel(HiddenRawMessage);
        }

        private Task<byte[]> PerformHiding()
        {
            return Task.Factory.StartNew(() => _selectedExecutableFilesMethod.PerformHiding(this));
        }

        public async Task Decode()
        {
            DecodedMessage = await PerformDecoding();
        }

        private Task<byte[]> PerformDecoding()
        {
            return Task.Factory.StartNew(() => _selectedExecutableFilesMethod.PerformDecoding(this));
        }

        public void OpenReadDialog()
        {
            ContainerRawMessage = _filePickerDialog.OpenReadDialog(_selectedExecutableFilesMethod.dialogType);
        }

        public void Save()
        {
            _filePickerDialog.OpenSaveDialog(_selectedExecutableFilesMethod.dialogType, HiddenRawMessage);
        }

        public void Clear()
        {
            ContainerRawMessage = null;
            MessageToHide = null;
            DecodedMessage = null;
            HiddenMessageViewModel = null;
            HiddenRawMessage = null;
        }
    }
}
