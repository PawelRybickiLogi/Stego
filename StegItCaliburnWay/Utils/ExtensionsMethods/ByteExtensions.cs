using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils.ExtensionsMethods
{
    public static class ByteExtensions
    {
        public static int GetIntFromByteArray(this byte[] array)
        {
            BitArray bitsToReturn = new BitArray(array.Length * 8);

            for (int i = 0; i < array.Length; i++)
            {
                var bitsFromByte = new BitArray(new[] { array[i] });

                for (int j = 0; j < 8; j++)
                {
                    bitsToReturn.Set((j + i * 8), bitsFromByte[j]);
                }
            }

            var result = bitsToReturn.GetIntFromBitArray();

            return result;
        }
    }
}
