using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    public class ShellViewModel : Conductor<IStegenographyMethodViewModel>.Collection.OneActive
    {
        private readonly TextViewModel _textViewModel;
        private readonly FilePickerDialog _filePickerDialog;

        public override string DisplayName
        {
            get { return "Dojebana apka Pawła Rybickiego"; }
            set { }
        }

        public ShellViewModel(
            ImageViewModel imageViewModel,
            TextViewModel textViewModel,
            ThirdViewModel thirdViewModel,
            FilePickerDialog filePickerDialog)
        {
            _textViewModel = textViewModel;
            _filePickerDialog = filePickerDialog;
            
            Items.AddRange(new IStegenographyMethodViewModel[]
            {
                imageViewModel, textViewModel, thirdViewModel
            });
        }

        public void ReadContainer()
        {
            try
            {
                _textViewModel.ContainerMessage = new string(_filePickerDialog.OpenReadDialog());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Hide()
        {
            this.ActiveItem.Hide();
        }
    }
}
