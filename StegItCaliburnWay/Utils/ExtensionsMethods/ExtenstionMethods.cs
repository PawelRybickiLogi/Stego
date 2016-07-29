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
using StegItCaliburnWay.Utils.ExtensionsMethods;
using ValueType = StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod.ValueType;

namespace StegItCaliburnWay.Utils
{
    public static class ExtenstionMethods
    {
       
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


        public static int SpaceCount(this char[] array)
        {
            const char SPACE = ' ';

            if (array != null)
            {
                return array.Count(m => m == SPACE);
            }

            return 0;
        }
    }
}
