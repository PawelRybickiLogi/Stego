using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Gif
{
    public class GifCoding : ImageCodingMethod
    {
        public ImageFile CreateHiddenMessage(Bitmap container, byte[] message)
        {
            throw new NotImplementedException();
        }

        public ImageFile DecodeHiddenMessage(Bitmap hiddenMessageContainer)
        {
            throw new NotImplementedException();
        }
    }
}
