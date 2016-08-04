using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegIt.Text.StegoTools;
using StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod;
using StegItCaliburnWay.Utils;
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
            get { return "Metoda semantyczna"; }
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
            get { return "Kodowanie spacją"; }
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

    public class CustomCodingMethod : TextMethod
    {
        private readonly CustomCoding _customCoding;

        public CustomCodingMethod(
            CustomCoding customCoding)
        {
            _customCoding = customCoding;
        }

        public override string Name
        {
            get { return "Autorska metoda"; }
        }

        public override byte[] PerformHiding(TextViewModel textViewModel)
        {
            return _customCoding.CreateHiddenMessage(textViewModel.ContainerRawMessage, textViewModel.MessageToHide);
        }

        public override byte[] PerformDecoding(TextViewModel textViewModel)
        {
            return _customCoding.DecodeHiddenMessage(textViewModel.ContainerRawMessage);
        }
    }
}
