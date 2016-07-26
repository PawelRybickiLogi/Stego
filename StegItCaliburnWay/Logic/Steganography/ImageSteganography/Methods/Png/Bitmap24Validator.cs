using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Bitmap24
{
    public class Bitmap24Validator
    {
        public void CheckIfCanHideMessageOrThrow(Bitmap containerBitmap, BitArray bitsFromFileToSave)
        {
            var containerPixelFormat = containerBitmap.PixelFormat;

            if (containerPixelFormat != PixelFormat.Format24bppRgb && containerPixelFormat != PixelFormat.Format32bppRgb)
                throw new Exception("Kontener posiada nieprawidłowy format!");

            var imageCapacity = containerBitmap.Height * containerBitmap.Width * Bitmap.GetPixelFormatSize(containerBitmap.PixelFormat);

            if (bitsFromFileToSave.Length > imageCapacity)
                throw new Exception("Wiadomość jest zbyt długa aby umieścić ją w obrazie" + Environment.NewLine +
                                    "Długość wiadomości w bajtach: " + bitsFromFileToSave.Length + Environment.NewLine +
                                    "Ilośc dostępnego miejsca w obrazie: " + imageCapacity);
        }
    }
}
