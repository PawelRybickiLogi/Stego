using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods
{
    interface ImageCodingMethod
    {
        byte[] CreateHiddenMessage(Bitmap container, byte[] message);
        byte[] DecodeHiddenMessage(byte[] openedFile);
    }
}
