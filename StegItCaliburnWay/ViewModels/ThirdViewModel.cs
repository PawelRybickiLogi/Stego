using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    public class ThirdViewModel : Screen, IStegenographyMethodViewModel
    {
        public override string DisplayName
        {
            get { return "Insze Gówno"; }
            set { }
        }

        public void Hide()
        {

        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void SaveToFile()
        {
            throw new NotImplementedException();
        }

        public void Decode()
        {
            throw new NotImplementedException();
        }

        public byte[] ContainerRawMessage { get; set; }
        public byte[] MessageToHide { get; set; }
        public byte[] HiddenMessage { get; set; }
    }
}
