using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegIt.Text.StegoTools;

namespace StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod
{
    public class CustomCoding : ITextCodingMethod
    {
        public byte[] CreateHiddenMessage(byte[] container, byte[] message)
        {
            var settings = new SettingsFrameFromContent(container);



            return null;
        }

        public byte[] DecodeHiddenMessage(byte[] openedFile)
        {
            throw new NotImplementedException();
        }
    }
}
