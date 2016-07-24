using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Bitmap32
{
    public class Bitmap32Validator
    {
        public void CheckIfCanHideMessageOrThrow(Bitmap containerBitmap, BitArray bitsFromFileToSave)
        {
/*            if (containerBitmap.PixelFormat != PixelFormat.Format32bppArgb && containerBitmap.PixelFormat != PixelFormat.Format32bppRgb)
                throw new Exception("Kontener posiada nieprawidłowy format!");
            */
            var imageCapacity = containerBitmap.Height*containerBitmap.Width*4;

            if (bitsFromFileToSave.Length > imageCapacity)
                throw new Exception("Wiadomość jest zbyt długa aby umieścić ją w obrazie " + Environment.NewLine +
                                    "Długość wiadomości w bajtach: " + bitsFromFileToSave.Length + Environment.NewLine +
                                    "Ilośc dostępnego miejsca w obrazie: " + imageCapacity);
        }
    }
}
