using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.ViewModels
{
    public class ShellViewModel : Conductor<IStegenographyMethodViewModel>.Collection.OneActive
    {

        private readonly FilePickerDialog _filePickerDialog;

        public override string DisplayName
        {
            get { return "StegIt - Paweł Rybicki"; }
            set { }
        }

        public ShellViewModel(
            ImageViewModel imageViewModel,
            TextViewModel textViewModel,
            ThirdViewModel thirdViewModel,
            FilePickerDialog filePickerDialog)
        {
            _filePickerDialog = filePickerDialog;
            
            Items.AddRange(new IStegenographyMethodViewModel[]
            {
                textViewModel, imageViewModel, thirdViewModel
            });
        }

        public void ReadContainer()
        {
            try
            {
                ActiveItem.OpenReadDialog();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void ReadMessageToHide()
        {
            try
            {
                ActiveItem.MessageToHide = _filePickerDialog.OpenReadDialog(DialogType.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Hide()
        {
            ActiveItem.Hide();
        }

        public void Decode()
        {
            ActiveItem.Decode();
        }

        public void Clear()
        {
            ActiveItem.HiddenMessage = null;
            ActiveItem.ContainerRawMessage = null;
            ActiveItem.MessageToHide = null;
        }

        public void SaveToFile()
        {
            _filePickerDialog.OpenSaveDialog(ActiveItem.HiddenMessage);
        }
    }
}
