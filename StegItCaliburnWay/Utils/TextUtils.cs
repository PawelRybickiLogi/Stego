using System.Collections;
using System.Text;

namespace StegItCaliburnWay
{
    class TextUtils
    {
        public static char[] GetUTF8CharArrayFromByteStream(byte[] bytes)
        {
            System.Text.Encoding inputEnc = System.Text.Encoding.UTF8;

            byte[] decoded = Encoding.Convert(inputEnc, inputEnc, bytes, 0, bytes.Length);

            char[] chars = Encoding.UTF8.GetString(decoded).ToCharArray();

            return chars;
        }

        public static BitArray GetMessageBitArray(byte[] message)
        {
            return new BitArray(message);
        }

        public static byte[] GetBytesFromMessage(char[] messageChars)
        {
            return Encoding.UTF8.GetBytes(messageChars);
        }
    }
}
