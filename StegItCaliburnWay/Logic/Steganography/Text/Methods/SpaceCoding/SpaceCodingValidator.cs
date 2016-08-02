using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.SpaceCoding
{
    public class SpaceCodingValidator
    {
        private const char SPACE = ' ';

        public void CheckIfCanHideMessageOrThrow(char[] container, byte[] message)
        {

            if (container.SpaceCount() < TextUtils.GetMessageBitArray(message).Length)
            {
                throw new Exception("Liczba spacji jest niewystarczająca aby ukryć wiadomość" + Environment.NewLine +
                    "Długość wiadomości w bitach: " + TextUtils.GetMessageBitArray(message).Length + Environment.NewLine +
                    "Ilość spacji w tekście: " + container.SpaceCount());
            }

        }
    }
}
