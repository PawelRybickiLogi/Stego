using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.Utils.ExtensionsMethods;

namespace StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod
{
    public class SettingsFrameFromContent
    {
        public int JumpValue;
        public ValueType ReturnValueType;
        public char Coding;
        public int Shift;
        public int MessageLength;

        public SettingsFrameFromContent() { }

        public static SettingsFrameFromContent GetMaxCapacitySettingsFrameForMessage(byte[] message)
        {
            return new SettingsFrameFromContent()
            {
                JumpValue = 0,
                ReturnValueType = ValueType.CHARS,
                Coding = CodingSign.SPACE,
                Shift = 0,
                MessageLength = TextUtils.GetMessageBitArray(message).Length
            };
        }

        public SettingsFrameFromContent(byte[] content)
        {
            char[] contentcik = TextUtils.GetUTF8CharArrayFromByteStream(content);

            BitArray contentSettingsBits = content.GetSettingsFrame();

            var jumpBits = contentSettingsBits.GetBitArrayFromBitArrayRange(0, 12);
            JumpValue = jumpBits.GetIntFromBitArray();

            var flags = contentSettingsBits.GetBitArrayFromBitArrayRange(12, 4);
            ReturnValueType = GetValueType(flags.Get(0));
            Coding = GetCodingSign(flags.Get(1));

            var shiftBits = contentSettingsBits.GetBitArrayFromBitArrayRange(16, 16);
            Shift = shiftBits.GetIntFromBitArray();

            var lengthBits = contentSettingsBits.GetBitArrayFromBitArrayRange(32, 32);
            MessageLength = lengthBits.GetIntFromBitArray(); ;
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
    }
}
