using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    class HintsViewModel : Screen
    {
        private byte[] _stegItImage;


        public HintsViewModel()
        {
            var image = new BitmapImage(new Uri("/StegItPng.png", UriKind.Relative));
        }

        public byte[] StegItImage
        {
            get { return _stegItImage; }
            set
            {
                _stegItImage = value;
                NotifyOfPropertyChange(() => StegItImage);
            }
        }
    }
}
