using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils.ExtensionsMethods
{
    public static class BitArrayExtensions
    {
        public static BitArray GetBitArrayFromBitArrayRange(this BitArray bitArray, int startIndex, int bitsToTake)
        {
            if (startIndex > bitArray.Length - bitsToTake)
                throw new Exception("wrong index value");

            BitArray tmpBits = new BitArray(bitsToTake);

            int counter = 0;
            for (int i = startIndex; counter < bitsToTake; i++)
            {
                tmpBits[counter] = bitArray.Get(i);
                counter++;
            }

            return tmpBits;
        }

        public static byte[] ToByteArray(this BitArray bits)
        {
            if (bits.Count % 8 != 0)
            {
                throw new ArgumentException("bits");
            }


            byte[] bytes = new byte[bits.Length / 8];
            bits.CopyTo(bytes, 0);
            return bytes;
        }

        public static int GetIntFromBitArray(this BitArray bitArray)
        {
            int value = 0;

            for (int i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                    value += Convert.ToInt32(Math.Pow(2, i));
            }

            return value;
        }

        public static int GetPositiveBitsCount(this BitArray array)
        {
            return array.Cast<object>().Count(bit => bit.Equals(true));
        }

        public static byte[] ToBytes(this BitArray array)
        {
            var messageBytes = new byte[array.Count];
            array.CopyTo(messageBytes, 0);

            return messageBytes;
        }

    }
}
