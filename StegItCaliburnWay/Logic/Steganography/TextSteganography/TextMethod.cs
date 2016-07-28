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
            get { return "Custom Coding"; }
        }

        public override byte[] PerformHiding(TextViewModel textViewModel)
        {

/*            var bits = new BitArray(64);
            bits.Set(0, true);
            bits.Set(1, false);
            bits.Set(2, false);
            bits.Set(3, false);
            bits.Set(4, false);
            bits.Set(5, false);
            bits.Set(6, false);
            bits.Set(7, false);
            bits.Set(8, false);
            bits.Set(9, false);
            bits.Set(10, false);
            bits.Set(11, false);

            bits.Set(12, false);
            bits.Set(13, true);
            bits.Set(14, false);
            bits.Set(15, false);

            bits.Set(16, false);
            bits.Set(17, true);
            bits.Set(18, false);
            bits.Set(19, true);
            bits.Set(20, false);
            bits.Set(21, false);
            bits.Set(22, true);
            bits.Set(23, false);
            bits.Set(24, false);
            bits.Set(25, false);
            bits.Set(26, false);
            bits.Set(27, false);
            bits.Set(28, false);
            bits.Set(29, false);
            bits.Set(30, false);
            bits.Set(31, false);

            bits.Set(32, false);
            bits.Set(33, false);
            bits.Set(34, false);
            bits.Set(35, false);
            bits.Set(36, false);
            bits.Set(37, false);
            bits.Set(38, false);
            bits.Set(39, false);
            bits.Set(40, false);
            bits.Set(31, false);
            bits.Set(32, false);
            bits.Set(33, false);
            bits.Set(34, false);
            bits.Set(35, false);
            bits.Set(36, false);
            bits.Set(37, false);
            bits.Set(38, false);
            bits.Set(39, false);
            bits.Set(40, false);
            bits.Set(41, false);
            bits.Set(42, false);
            bits.Set(43, false);
            bits.Set(44, true);
            bits.Set(45, false);
            bits.Set(46, false);
            bits.Set(47, false);
            bits.Set(48, false);
            bits.Set(49, false);
            bits.Set(40, false);
            bits.Set(51, true);
            bits.Set(52, false);
            bits.Set(53, false);
            bits.Set(54, false);
            bits.Set(55, false);
            bits.Set(56, true);
            bits.Set(57, false);
            bits.Set(58, false);
            bits.Set(59, false);
            bits.Set(60, false);
            bits.Set(61, true);
            bits.Set(62, false);
            bits.Set(63, false);*/

            return _customCoding.CreateHiddenMessage(textViewModel.ContainerRawMessage, textViewModel.MessageToHide);
        }

        public override byte[] PerformDecoding(TextViewModel textViewModel)
        {
            return _customCoding.DecodeHiddenMessage(textViewModel.ContainerRawMessage);
        }
    }
}
