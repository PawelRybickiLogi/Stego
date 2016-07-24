using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods
{
    interface ImageCodingMethod
    {
        ImageFile CreateHiddenMessage(Bitmap container, byte[] message);
        ImageFile DecodeHiddenMessage(Bitmap hiddenMessageContainer);
    }
}
