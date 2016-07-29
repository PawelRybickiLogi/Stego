using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Gif
{
    public class GifCodingValidator
    {

        public void CheckIfCanHideMessageOrThrow(Bitmap container, BitArray bitsFromMessageToSave)
        {
            var containerPixelFormat = container.PixelFormat;

            if (containerPixelFormat != PixelFormat.Format8bppIndexed)
                throw new Exception("Kontener posiada nieprawidłowy format! Wymagany format to 8 bitowy indeksowany GIF");

            if (container.Palette.Entries.Length < 128)
                throw new Exception("Obraz GIF posiada bardzo małą maletę kolorów (64 bajtów lub mniej). " + Environment.NewLine +
                                    "Zaleca się stosowanie przynajmniej 128 lub 256 bajtowej palety kolorów");

            var imageCapacity = container.Height * container.Width;

            if (bitsFromMessageToSave.Length > imageCapacity)
                throw new Exception("Wiadomość jest zbyt długa aby umieścić ją w obrazie" + Environment.NewLine +
                                    "Długość wiadomości w bajtach: " + bitsFromMessageToSave.Length + Environment.NewLine +
                                    "Ilośc dostępnego miejsca w obrazie: " + imageCapacity);
        }
    }
}
