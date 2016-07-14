using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegIt.Text
{
    class TextUtils
    {
        public static char[] GetUTF8CharArrayFromByteStream(byte[] bytes)
        {
            System.Text.Encoding inputEnc = System.Text.Encoding.UTF8;
            //System.Text.Encoding outputEnc = System.Text.Encoding.Default;

            byte[] decoded = Encoding.Convert(inputEnc, inputEnc, bytes, 0, bytes.Length);

            char[] chars = Encoding.UTF8.GetString(decoded).ToCharArray();

            return chars;
        }

        public static BitArray GetMessageBitArray(byte[] message)
        {
            return new BitArray(message);
        }
    }
}
