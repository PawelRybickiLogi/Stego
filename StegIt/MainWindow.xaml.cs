using System.Windows.Controls;
using StegIt.Dialog;
using System.Windows;
using StegIt.Text;
using StegIt.Text.StegoTools;

namespace StegIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ApplicationBindingContext _binding;
        private StegoTextStrategy stegoTextStrategy = new StegoTextStrategy();

        public MainWindow()
        {
            InitializeComponent();

            _binding = new ApplicationBindingContext();
            DataContext = _binding;
        }

        private void ReadContainer_ButtonClick(object sender, RoutedEventArgs e)
        {
            var message = FilePickerDialog.OpenReadDialog();
            _binding.ContainerMessage = new string(message);
        }

        private void ReadMsgToHide_ButtonClick(object sender, RoutedEventArgs e)
        {
            var message = FilePickerDialog.OpenReadDialog();
            _binding.MessageToHide = new string(message);
        }

        private void SaveFile_ButtonClick(object sender, RoutedEventArgs e)
        {
            FilePickerDialog.OpenSaveDialog(_binding._hiddenMessageValue);
        }

        private void Hide_ButtonClick(object sender, RoutedEventArgs e)
        {
            var message = stegoTextStrategy.PerformHidingBasedOnCurrentSelectedMethod(_binding);

            _binding.HiddenMessage = new string(message); //.Replace("\0", string.Empty);
        }

        private void Decode_ButtonClick(object sender, RoutedEventArgs e)
        {
            var message = stegoTextStrategy.PerformDecodingBasedOnCurrentSelectedMethod(_binding);

            _binding.HiddenMessage = new string(message).Replace("\0", string.Empty);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string optionSelected = (sender as ComboBox).SelectedItem.ToString();
            _binding.SelectedImageOption = optionSelected;
        }

        private void Clear_ButtonClick(object sender, RoutedEventArgs e)
        {
            _binding.ClearAll();
        }
    }
}
