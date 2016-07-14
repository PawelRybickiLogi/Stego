using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    public class ImageViewModel : Screen, IStegenographyMethodViewModel
    {
        public override string DisplayName
        {
            get { return "Obraz"; }
            set { }
        }

        public void Hide()
        {
            
        }
    }
}
