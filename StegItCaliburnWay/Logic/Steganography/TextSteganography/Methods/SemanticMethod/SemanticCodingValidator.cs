using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegIt.Text.StegoTools.SemanticMethod;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.SemanticMethod
{
    public class SemanticCodingValidator
    {
        public void CheckIfCanHideMessageOrThrow(char[] container, byte[] message)
        {
            Dictionary<char, char> listOfLettersThatCanBeChanged = SemanticLettersValues.GetLetters();

            var numberOfLettersInContainerThatCanBeChanged = container.Count(listOfLettersThatCanBeChanged.ContainsKey);

            if (numberOfLettersInContainerThatCanBeChanged < TextUtils.GetMessageBitArray(message).Length)
            {
                throw new Exception("Liczba znaków możliwych do podmienienia jest niewystarczająca" + Environment.NewLine +
                    "Długość wiadomości w bitach: " + TextUtils.GetMessageBitArray(message).Length + Environment.NewLine +
                    "Ilość znaków w tekście, które można podmienić: " + numberOfLettersInContainerThatCanBeChanged);
            }
        }
    }
}
