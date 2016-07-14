using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;

namespace StegItCaliburnWay.ViewModels
{
    public abstract class TextMethod
    {
        public abstract string Name { get; }

        public abstract void Perform();
    }

    class SemanticCoding : TextMethod
    {
        public override string Name
        {
            get { return "Semantic Method"; }
        }


        public override void Perform()
        {
            throw new NotImplementedException();
        }
    }

    class WhiteSpaceCoding : TextMethod
    {

        public override string Name
        {
            get { return "Whitespace Coding"; }
        } 

        public override void Perform()
        {
            throw new NotImplementedException();
        }
    }

    public class TextViewModel : Screen, IStegenographyMethodViewModel
    {
        public TextViewModel()
        {
            TextMethods = new List<TextMethod>
            {
                new SemanticCoding(),
                new WhiteSpaceCoding()
            };

            SelectedTextMethod = TextMethods[0];
        }


        public override string DisplayName
        {
            get { return "Tekst"; }
            set { }
        }

        public void Hide()
        {
            
        }

        public List<TextMethod> TextMethods { get; set; } 

        private TextMethod _selectedTextMethod;

        public TextMethod SelectedTextMethod
        {
            get { return _selectedTextMethod; }
            set
            {
                _selectedTextMethod = value;
                NotifyOfPropertyChange(() => SelectedTextMethod);
            }
        }

        private string _containerMessage;

        public string ContainerMessage
        {
            get { return _containerMessage; }
            set
            {
                _containerMessage = value;
                NotifyOfPropertyChange(() => ContainerMessage);
            }
        }

        public void TextMethodChanged(SelectionChangedEventArgs eventArgs)
        {
            
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }
    }
}
