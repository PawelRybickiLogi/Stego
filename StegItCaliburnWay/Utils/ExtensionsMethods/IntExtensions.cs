using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils.ExtensionsMethods
{
    public static class IntExtensions
    {
        private static Dictionary<int, int> shiftDictionary = new Dictionary<int, int>()
        {
            {2, 0x000F},
            {4, 0x00FF},
            {6, 0x0FFF},
            {8, 0xFFFF},
        };
     
        public static uint PutBitsAsLSB(this uint value, BitArray bitsToPut)
        {

            int result = (int)value & shiftDictionary[bitsToPut.Length];

            value = value - (uint) result;

            var valueToAdd = (uint) bitsToPut.GetIntFromBitArray();

            return value + valueToAdd;
        }

        public static int GetLastNBitsIntValue(this uint value, int numberOfBits)
        {
            return (int) value & shiftDictionary[numberOfBits];
        }

        public static int DivideRoundingUp(this int x, int y)
        {
            int remainder;
            int quotient = Math.DivRem(x, y, out remainder);
            return remainder == 0 ? quotient : quotient + 1;
        }

        public static byte[] GetBitsAs4ByteArray(this int x)
        {
            byte[] bytesToReturn = new byte[4];

            BitArray bits = new BitArray(new[] { x });

            for (int i = 0; i < 4; i++)
            {
                var oneByteFromBits = bits.GetBitArrayFromBitArrayRange(i * 8, 8);
                var intFromArray = oneByteFromBits.GetIntFromBitArray();

                bytesToReturn[i] = (byte) intFromArray;
            }

            return bytesToReturn;
        }
    }

    
}
