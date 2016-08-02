using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    public class HiddenMessageTextViewModel : Screen
    {
        private string _text;

        public string Text
        {
            get { return _text; }
        }

        public HiddenMessageTextViewModel(string text)
        {
            _text = text;
        }

        public void Clear()
        {
            _text = "";
            
            NotifyOfPropertyChange(()=>Text);
        }
    }
}
