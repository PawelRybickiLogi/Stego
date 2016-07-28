using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
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

            UpdateUI();
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

            UpdateUI();
        }

        public void Hide()
        {
            ActiveItem.Hide();
            UpdateUI();
        }

        public void Decode()
        {
            ActiveItem.Decode();
        }

        public void Clear()
        {
            ActiveItem.HiddenRawMessage = null;
            ActiveItem.ContainerRawMessage = null;
            ActiveItem.MessageToHide = null;
            ActiveItem.DecodedMessage = null;

            UpdateUI();
        }

        public void SaveToFile()
        {
            try
            {
                ActiveItem.Save();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Problem z dostępem do pliku!");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Nieprawidłowa nazwa pliku");
            }
        }

        public bool ShouldEnableHide
        {
            get { return ActiveItem.ContainerRawMessage != null && ActiveItem.MessageToHide != null; }
        }

        public bool ShouldEnableSaveToFileFile
        {
            get { return ActiveItem.HiddenRawMessage != null; }
        }

        public bool ShouldEnableShowHiddenMessage
        {
            get { return ActiveItem.ContainerRawMessage != null; }
        }

        private void UpdateUI()
        {
            NotifyOfPropertyChange(() => ShouldEnableHide);
            NotifyOfPropertyChange(() => ShouldEnableSaveToFileFile);
            NotifyOfPropertyChange(() => ShouldEnableShowHiddenMessage);
        }
    }
}
