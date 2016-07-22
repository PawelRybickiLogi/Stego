using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Bitmap24
{
    public class Bitmap24Validator
    {

        public void CheckIfCanHideMessageOrThrow(CustomBitmap containerBitmap, BitArray bitsFromFileToSave)
        {
            if (containerBitmap.PixelFormat != PixelFormat.Format24bppRgb)
                throw new Exception("Invalid format");

            if (bitsFromFileToSave.Length > containerBitmap.Height * containerBitmap.Width * 3)
                throw new Exception("Message too long");
        }
    }
}
