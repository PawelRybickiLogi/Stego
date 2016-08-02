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
            VideoViewModel videoViewModel,
            AudioViewModel soundViewModel,
            FilePickerDialog filePickerDialog)
        {
            _filePickerDialog = filePickerDialog;
            
            Items.AddRange(new IStegenographyMethodViewModel[]
            {
                textViewModel, imageViewModel, soundViewModel, videoViewModel
            });

        }

        public void ReadContainer()
        {
            try
            {
                ActiveItem.OpenReadDialog();
            }
            catch (ArgumentException ex) { }
            catch (Exception ex)
            {
                MessageBox.Show("Nieprawidłowa nazwa pliku");
            }

            UpdateUI();
        }

        public void ReadMessageToHide()
        {
            try
            {
                var message = _filePickerDialog.OpenReadDialog(DialogType.Text);

                if (message != null)
                    ActiveItem.MessageToHide = message;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            UpdateUI();
        }

        public async void Hide()
        {
            LoaderVisibility = Visibility.Visible;

            try
            {
                await ActiveItem.Hide();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                UpdateUI();
                LoaderVisibility = Visibility.Collapsed;
            }
        }

        public async void Decode()
        {
            LoaderVisibility = Visibility.Visible;

            try
            {
                await ActiveItem.Decode();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                UpdateUI();
                LoaderVisibility = Visibility.Collapsed;
            }
        }

        private Visibility _loaderVisibility = Visibility.Collapsed;
        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                NotifyOfPropertyChange(()=>LoaderVisibility);
            }
        }

        public void Clear()
        {
            ActiveItem.Clear();

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
