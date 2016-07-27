using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod
{
    class SettingsFrameFromContent
    {
        public int JumpValue;
        public ValueType ReturnValueType;
        public char Coding;
        public int Shift;
        public int MessageLength;

        public SettingsFrameFromContent(byte[] content)
        {
            BitArray contentBits = new BitArray(content);

            var jumpBits = contentBits.GetBitArrayFromBitArrayRange(0, 12);
            JumpValue = GetIntFromByteArray(jumpBits);

            var flags = contentBits.GetBitArrayFromBitArrayRange(12, 4);
            ReturnValueType = GetValueType(flags.Get(0));
            Coding = GetCodingSign(flags.Get(1));

            Shift = GetIntFromByteArray(contentBits.GetBitArrayFromBitArrayRange(16, 16));
            MessageLength = GetIntFromByteArray(contentBits.GetBitArrayFromBitArrayRange(32, 32));
        }

        public int GetJump()
        {
            return JumpValue;
        }

        private char GetCodingSign(bool flagValue)
        {
            return flagValue == false
                ? CodingSign.SPACE
                : CodingSign.JOINER;
        }

        private ValueType GetValueType(bool flagValue)
        {
            return flagValue == false
                ? ValueType.CHARS
                : ValueType.BITS;
        }

        private int GetIntFromByteArray(BitArray bitArray)
        {
            int value = 0;

            for (int i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                    value += Convert.ToInt32(Math.Pow(2, i));
            }

            return value;
        }
    }
}
