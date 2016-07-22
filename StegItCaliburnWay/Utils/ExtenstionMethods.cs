using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils
{
    public static class ExtenstionMethods
    {
        public static Color ReplaceColorPixel(this Color colorValue, bool newRed, bool newGreen, bool newBlue)
        {
            byte newBlueValue = newBlue
                ? (byte)(colorValue.B | 1)     //LSB 1
                : (byte)(colorValue.B & 254);  //LSB 0

            byte newGreenValue = newGreen
                ? (byte)(colorValue.G | 1)     //LSB 1
                : (byte)(colorValue.G & 254);  //LSB 0

            byte newRedValue = newRed
                ? (byte)(colorValue.R | 1)     //LSB 1
                : (byte)(colorValue.R & 254);  //LSB 0

            return Color.FromArgb(newRedValue, newGreenValue, newBlueValue);
        }

        public static Color Replace1ColorBit(this Color colorValue, bool newRed)
        {
            byte newRedValue = newRed
                ? (byte)(colorValue.R | 1)     //LSB 1
                : (byte)(colorValue.R & 254);  //LSB 0

            return Color.FromArgb(newRedValue, colorValue.G, colorValue.B);
        }

        public static Color Replace2ColorBits(this Color colorValue, bool newRed, bool newGreen)
        {
            byte newRedValue = newRed
                ? (byte)(colorValue.R | 1)     //LSB 1
                : (byte)(colorValue.R & 254);  //LSB 0

            byte newGreenValue = newGreen
                ? (byte)(colorValue.G | 1)     //LSB 1
                : (byte)(colorValue.G & 254);  //LSB 0

            return Color.FromArgb(newRedValue, newGreenValue, colorValue.B);
        }
    }
}
