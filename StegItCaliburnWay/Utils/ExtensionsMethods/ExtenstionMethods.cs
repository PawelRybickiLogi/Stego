using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod;
using ValueType = StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod.ValueType;

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

        public static bool IsIndextedImage(this Image img)
        {
            return img.PixelFormat == PixelFormat.Format8bppIndexed;
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

        public static BitArray ToBitArray(this SettingsFrameFromContent frame)
        {

            if((frame.Coding != CodingSign.JOINER && frame.Coding != CodingSign.SPACE) ||
                (frame.ReturnValueType != ValueType.BITS && frame.ReturnValueType != ValueType.CHARS) ||
                frame.MessageLength == 0)
                throw new Exception("Frame not set correctly");

            var settingsBits = new BitArray(64);

            BitArray jumpBits = new BitArray(new[] { frame.JumpValue });

            for (int i = 0; i < 12; i++)
            {
                settingsBits.Set(i, jumpBits.Get(i));
            }

            settingsBits.Set(12, frame.ReturnValueType == ValueType.BITS);
            settingsBits.Set(13, frame.Coding == CodingSign.JOINER);
            settingsBits.Set(14, false);
            settingsBits.Set(15, false);

            BitArray shiftBits = new BitArray(new[] { frame.Shift });

            for (int i = 0; i < 16; i++)
            {
                settingsBits.Set(i + 16, shiftBits.Get(i));
            }

            BitArray lengthBits = new BitArray(new[] { frame.MessageLength });

            for (int i = 0; i < 32; i++)
            {
                settingsBits.Set(i + 32, lengthBits.Get(i));
            }

            return settingsBits;
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

        public static BitArray GetSettingsFrame(this byte[] bytes)
        {
            BitArray settingsBits = new BitArray(64);
            var chars = TextUtils.GetUTF8CharArrayFromByteStream(bytes);

            var insertedBits = 0;

            for (int i = 0; i < 64; i++)
            {
                if (chars[i + insertedBits] == CodingSign.SPACE)
                {
                    settingsBits.Set(i, true);
                    insertedBits++;
                }
                else
                {
                    settingsBits.Set(i, false);
                }
            }

            return settingsBits;
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

        public static int SpaceCount(this char[] array)
        {
            const char SPACE = ' ';

            if (array != null)
            {
                return array.Count(m => m == SPACE);
            }

            return 0;
        }

        public static List<byte> GetAllPixelsIndexes(this BitmapData bmd)
        {
            var references = new List<byte>();

            for (int i = 0; i < bmd.Width; i++)
            {
                for (int j = 0; j < bmd.Height; j++)
                {
                    references.Add(bmd.GetPixel(i, j));
                }
            }

            return references;
        }

        public static unsafe void SetPixel(this BitmapData bmd, int x, int y, byte c)
        {
            byte* p = (byte*)bmd.Scan0.ToPointer();
            int offset = y * bmd.Stride + (x);
            p[offset] = c;
        }

        public static unsafe Byte GetPixel(this BitmapData bmd, int x, int y)
        {
            byte* p = (byte*)bmd.Scan0.ToPointer();
            int offset = y * bmd.Stride + x;
            return p[offset];
        }

        public static void MovePixelsToFirstHalfOfThePalette(this BitmapData bitmapData, Dictionary<byte, byte> oldToNewIndexMap, List<Color> first64EntriesFromPalette, Dictionary<byte, Color> originalPalleteColorForIndex)
        {
            for (int i = 0; i < bitmapData.Width; i++)
            {
                for (int j = 0; j < bitmapData.Height; j++)
                {
                    var oldPixelIndex = bitmapData.GetPixel(i, j);

                    if (oldToNewIndexMap.ContainsKey(oldPixelIndex))
                    {
                        bitmapData.SetPixel(i, j, oldToNewIndexMap[oldPixelIndex]);
                    }
                    else
                    {
                        var closestColorIndexInNewPalette = GetClosestColorIndex(first64EntriesFromPalette, originalPalleteColorForIndex[oldPixelIndex]);
                        bitmapData.SetPixel(i, j, (byte) closestColorIndexInNewPalette);
                    }
                }
            }
        }

        private static int GetClosestColorIndex(IEnumerable<Color> colorArray, Color baseColor)
        {
            var colors = colorArray.Select(x => new { Value = x, Diff = GetDiff(x, baseColor) }).ToList();
            var min = colors.Min(x => x.Diff);
            return colors.FindIndex(x => x.Diff == min);
        }

        private static int GetDiff(Color color, Color baseColor)
        {
            int a = color.A - baseColor.A,
                r = color.R - baseColor.R,
                g = color.G - baseColor.G,
                b = color.B - baseColor.B;
            return a * a + r * r + g * g + b * b;
        }
    }
}
