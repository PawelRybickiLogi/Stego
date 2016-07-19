using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegIt.Text.StegoTools;
using StegItCaliburnWay.ViewModels;

namespace StegItCaliburnWay.Logic.TextSteganography
{
    public abstract class TextMethod
    {
        public abstract string Name { get; }
        public abstract byte[] PerformDecoding(TextViewModel textViewModel);
        public abstract byte[] PerformHiding(TextViewModel textViewModel);
    }

    public class SemanticCodingMethod : TextMethod
    {
        private readonly SemanticCoding _semanticCoding;

        public SemanticCodingMethod(
            SemanticCoding semanticCoding)
        {
            _semanticCoding = semanticCoding;
        }

        public override string Name
        {
            get { return "Semantic Method"; }
        }


        public override byte[] PerformHiding(TextViewModel textViewModel)
        {
            return _semanticCoding.CreateHiddenMessage(textViewModel.ContainerRawMessage, textViewModel.MessageToHide);
        }

        public override byte[] PerformDecoding(TextViewModel textViewModel)
        {
            return _semanticCoding.DecodeHiddenMessage(textViewModel.ContainerRawMessage);
        }
    }

    public class WhiteSpaceCodingMethod : TextMethod
    {
        private readonly SpaceCoding _spaceCoding;

        public WhiteSpaceCodingMethod(
            SpaceCoding spaceCoding)
        {
            _spaceCoding = spaceCoding;
        }

        public override string Name
        {
            get { return "Whitespace Coding"; }
        }

        public override byte[] PerformHiding(TextViewModel textViewModel)
        {
            return _spaceCoding.CreateHiddenMessage(textViewModel.ContainerRawMessage, textViewModel.MessageToHide);
        }

        public override byte[] PerformDecoding(TextViewModel textViewModel)
        {
            return _spaceCoding.DecodeHiddenMessage(textViewModel.ContainerRawMessage);
        }
    }
}
