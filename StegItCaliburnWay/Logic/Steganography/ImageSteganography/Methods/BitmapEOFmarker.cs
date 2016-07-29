using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.Utils.ExtensionsMethods;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods
{
    public class BitmapEOFmarker
    {
        //private static readonly byte[] markup = {95, 115, 105, 95}; //_si_

        private static readonly byte[] markup = { 100 }; //d

        private static readonly BitArray EOF_MARKUP = new BitArray(markup);
        private static readonly short BITS_FOR_PIXEL = 4;
        private readonly short pixelsToMarkEnding = (short) (EOF_MARKUP.Length / BITS_FOR_PIXEL);

        public void markEndOfImageMessageIn32BitImage(int x, int y, Bitmap containerDeepCopy)
        {
            if (x == containerDeepCopy.Width)
            {
                x = 0;
                y++;
            }

            var pixelsEdited = 0;

            for (; y < containerDeepCopy.Height; y++)
            {
                if (pixelsEdited >= pixelsToMarkEnding)
                    return;

                for (; x < containerDeepCopy.Width; x++)
                {
                    if(pixelsEdited >= pixelsToMarkEnding)
                        return;

                    var pixelColor = containerDeepCopy.GetPixel(x, y);

                    var pixelColorAfterHidingBits = pixelColor.ReplaceColorPixel(
                        EOF_MARKUP.Get(0 + pixelsEdited * BITS_FOR_PIXEL),     //ALPHA
                        EOF_MARKUP.Get(1 + pixelsEdited * BITS_FOR_PIXEL),     //RED
                        EOF_MARKUP.Get(2 + pixelsEdited * BITS_FOR_PIXEL),     //GREEN
                        EOF_MARKUP.Get(3 + pixelsEdited * BITS_FOR_PIXEL));    //BLUE

                    containerDeepCopy.SetPixel(x, y, pixelColorAfterHidingBits);

                    pixelsEdited++;
                }
            }
        }

        public int GetNumberOfBytesToReadFromImage(Bitmap hiddenMessageContainer)
        {
            var hiddenImagePixels = hiddenMessageContainer.Width*hiddenMessageContainer.Height;

            var imageBytes = ImageUtils.BitmapToBytes(hiddenMessageContainer);
            for (int i = 0; i < hiddenImagePixels * Bitmap.GetPixelFormatSize(hiddenMessageContainer.PixelFormat); i++)
            {
                if (imageBytes.Skip(i).Take(markup.Length).SequenceEqual(markup))
                {
                    return i * Bitmap.GetPixelFormatSize(hiddenMessageContainer.PixelFormat);
                }
            }

            return 0;
        }
    }
}
