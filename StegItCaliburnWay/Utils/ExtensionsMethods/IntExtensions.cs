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
    }

    
}
