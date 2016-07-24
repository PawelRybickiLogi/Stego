using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        public static Color ReplaceColorPixel(this Color colorValue, bool newAlpha, bool newRed, bool newGreen, bool newBlue)
        {
            byte newAlphaValue = newAlpha
                ? (byte)(colorValue.A | 1)     //LSB 1
                : (byte)(colorValue.A & 254);  //LSB 0

            byte newBlueValue = newBlue
                ? (byte)(colorValue.B | 1)     //LSB 1
                : (byte)(colorValue.B & 254);  //LSB 0

            byte newGreenValue = newGreen
                ? (byte)(colorValue.G | 1)     //LSB 1
                : (byte)(colorValue.G & 254);  //LSB 0

            byte newRedValue = newRed
                ? (byte)(colorValue.R | 1)     //LSB 1
                : (byte)(colorValue.R & 254);  //LSB 0

            return Color.FromArgb(newAlphaValue, newRedValue, newGreenValue, newBlueValue);
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

        public static bool HasAlphaChannel(this Image img)
        {
            return img.PixelFormat == PixelFormat.Format32bppArgb;
        }

        public static bool Is32BitImage(this Image img)
        {
            return img.PixelFormat == PixelFormat.Format32bppArgb || img.PixelFormat == PixelFormat.Format32bppRgb;
        }

        public static byte BitsForPixel(this Image img)
        {
            return (byte) (Bitmap.GetPixelFormatSize(img.PixelFormat) / 8);
        }

        public static void SaveAsNot32BitImage(this Image img, string fname)
        {
            var bmp = new Bitmap(img.Width, img.Height, img.PixelFormat);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));

            ImageConverter imgCon = new ImageConverter();
            var bytes = (byte[])imgCon.ConvertTo(bmp, typeof(byte[]));
            File.WriteAllBytes(fname, bytes);
        }
   
    }
}
