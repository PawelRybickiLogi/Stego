using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegIt.Text.StegoTools;

namespace StegIt.Text
{
    class StegoTextStrategy
    {
        readonly SpaceCoding spaceCoding = new SpaceCoding();
        readonly SemanticCoding semanticCoding = new SemanticCoding();

        public char[] PerformHidingBasedOnCurrentSelectedMethod(ApplicationBindingContext bindingContext)
        {
            char[] message = null;

            if (bindingContext.SelectedImageOption == ImageOption.WHITESPACE_CODING)
                message = spaceCoding.CreateHiddenMessage(bindingContext._containerMessageValue, bindingContext._messageToHideValue);
            else if(bindingContext.SelectedImageOption == ImageOption.SEMANTIC_CODING)
                message = semanticCoding.CreateHiddenMessage(bindingContext._containerMessageValue, bindingContext._messageToHideValue);

            return message;
        }

        public char[] PerformDecodingBasedOnCurrentSelectedMethod(ApplicationBindingContext bindingContext)
        {
            char[] message = null;

            if (bindingContext.SelectedImageOption == ImageOption.WHITESPACE_CODING)
                message = spaceCoding.DecodeHiddenMessage(bindingContext._containerMessageValue);
            else if (bindingContext.SelectedImageOption == ImageOption.SEMANTIC_CODING)
                message = semanticCoding.DecodeHiddenMessage(bindingContext._containerMessageValue);

            return message;
        }
    }
}
