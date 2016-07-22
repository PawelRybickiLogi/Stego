using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.ViewModels
{
    public class ThirdViewModel : Screen, IStegenographyMethodViewModel
    {
        private readonly FilePickerDialog _filePickerDialog;
        private byte[] _containerRawMessage;
        private byte[] _messageToHide;
        private byte[] _hiddenMessage;

        public ThirdViewModel(FilePickerDialog filePickerDialog)
        {
            _filePickerDialog = filePickerDialog;
        }

        public override string DisplayName
        {
            get { return "Trzecia metoda"; }
            set { }
        }

        public byte[] ContainerRawMessage { get; set; }
        public byte[] MessageToHide { get; set; }
        public byte[] HiddenMessage { get; set; }
        public void OpenReadDialog()
        {
            ContainerRawMessage = _filePickerDialog.OpenReadDialog(DialogType.Image);
        }

        public void Hide()
        {

        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Decode()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
