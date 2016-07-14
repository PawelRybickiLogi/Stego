using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StegIt.Text
{
    class ApplicationBindingContext : INotifyPropertyChanged
    {
        public char[] _containerMessageValue;
        public char[] _messageToHideValue;
        public char[] _hiddenMessageValue;
        public string _selectedImageOption;
       
        public ObservableCollection<string> ImageMethods { get; set; } 

        public event PropertyChangedEventHandler PropertyChanged;


        public void ClearAll()
        {
            ContainerMessage = "";
            MessageToHide = "";
            HiddenMessage = "";
        }

        public ApplicationBindingContext()
        {
            ImageMethods = new ObservableCollection<string>
            {
                ImageOption.WHITESPACE_CODING,
                ImageOption.SEMANTIC_CODING
            };

            SelectedImageOption = ImageOption.WHITESPACE_CODING;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string SelectedImageOption
        {
            get { return _selectedImageOption; }
            set
            {
                _selectedImageOption = value;
                NotifyPropertyChanged();
            }
        }

        public string ContainerMessage
        {
            get { return new string(_containerMessageValue); }
            set
            {
                _containerMessageValue = value.ToArray();
                NotifyPropertyChanged();
            }
        }

        public string MessageToHide
        {
            get { return new string(_messageToHideValue); }
            set
            {
                _messageToHideValue = value.ToArray();
                NotifyPropertyChanged();
            }
        }
        public string HiddenMessage
        {
            get { return new string(_hiddenMessageValue); }
            set
            {
                _hiddenMessageValue = value.ToArray();
                NotifyPropertyChanged();
            }
        }
    }
}
