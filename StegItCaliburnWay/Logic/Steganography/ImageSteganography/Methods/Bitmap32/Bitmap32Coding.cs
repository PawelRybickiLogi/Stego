using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Bitmap32;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Bitmap24
{
    public class Bitmap32Coding : ImageCodingMethod
    {
        private const byte BITS_FOR_PIXEL = 4;

        private readonly Bitmap32Validator _bitmap32Validator;
        private readonly BitmapEOFmarker _bitmapEoFmarker;

        public Bitmap32Coding(
            Bitmap32Validator bitmap32Validator,
            BitmapEOFmarker bitmapEoFmarker)
        {
            _bitmap32Validator = bitmap32Validator;
            _bitmapEoFmarker = bitmapEoFmarker;
        }

        public ImageFile CreateHiddenMessage(Bitmap container, byte[] message)
        {
            var bitsFromFileToSave = TextUtils.GetMessageBitArray(message);

            _bitmap32Validator.CheckIfCanHideMessageOrThrow(container, bitsFromFileToSave);

            var px1 = container.GetPixel(0, 0);
            var px2 = container.GetPixel(1, 0);
            var px3 = container.GetPixel(2, 0);
            var px4 = container.GetPixel(0, 1);
            var px5 = container.GetPixel(1, 1);
            var px6 = container.GetPixel(2, 1);
            var px7 = container.GetPixel(0, 2);
            var px8 = container.GetPixel(1, 2);
            var px9 = container.GetPixel(2, 2);


            var containerDeepCopy = container.Clone(new Rectangle(0, 0, container.Width, container.Height), container.PixelFormat);

            var pixelsToEdit = bitsFromFileToSave.Length / BITS_FOR_PIXEL;
            var pixelsEdited = 0;

            int x = 0, y = 0;
            for (;y < containerDeepCopy.Height; y++)
            {
                if (pixelsEdited >= pixelsToEdit)
                {
                    //_bitmapEoFmarker.markEndOfImageMessageIn32BitImage(x, y - 1, containerDeepCopy);
                    break;
                }

                for (; x < containerDeepCopy.Width; x++)
                {
                    if (pixelsEdited < pixelsToEdit)
                    {
                        var pixelColor = containerDeepCopy.GetPixel(x, y);

                        var pixelColorAfterHidingBits = pixelColor.ReplaceColorPixel(
                            bitsFromFileToSave.Get(0 + pixelsEdited * BITS_FOR_PIXEL),     //ALPHA
                            bitsFromFileToSave.Get(1 + pixelsEdited * BITS_FOR_PIXEL),     //RED
                            bitsFromFileToSave.Get(2 + pixelsEdited * BITS_FOR_PIXEL),     //GREEN
                            bitsFromFileToSave.Get(3 + pixelsEdited * BITS_FOR_PIXEL));    //BLUE

                        containerDeepCopy.SetPixel(x, y, pixelColorAfterHidingBits);

                        pixelsEdited++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return new ImageFile(containerDeepCopy, ImageUtils.BitmapToBytes(containerDeepCopy));
        }

        public ImageFile DecodeHiddenMessage(Bitmap hiddenMessageContainer)
        {
            var messageBits = new BitArray(hiddenMessageContainer.Width * hiddenMessageContainer.Height * BITS_FOR_PIXEL);
            //var bytesToRead = _bitmapEoFmarker.GetNumberOfBytesToReadFromImage(hiddenMessageContainer);

            var px1 = hiddenMessageContainer.GetPixel(0, 0);
            var px2 = hiddenMessageContainer.GetPixel(1, 0);
            var px3 = hiddenMessageContainer.GetPixel(2, 0);
            var px4 = hiddenMessageContainer.GetPixel(0, 1);
            var px5 = hiddenMessageContainer.GetPixel(1, 1);
            var px6 = hiddenMessageContainer.GetPixel(2, 1);
            var px7 = hiddenMessageContainer.GetPixel(0, 2);
            var px8 = hiddenMessageContainer.GetPixel(1, 2);
            var px9 = hiddenMessageContainer.GetPixel(2, 2);


            var bitInputNumber = 0;

            for (int i = 0; i < hiddenMessageContainer.Height; i++)
            {
                for (int j = 0; j < hiddenMessageContainer.Width; j++)
                {
                    var pixelColor = hiddenMessageContainer.GetPixel(j, i);

                    messageBits.Set(bitInputNumber, IsOdd(pixelColor.A));
                    messageBits.Set(bitInputNumber + 1, IsOdd(pixelColor.R));
                    messageBits.Set(bitInputNumber + 2, IsOdd(pixelColor.G));
                    messageBits.Set(bitInputNumber + 3, IsOdd(pixelColor.B));

                    bitInputNumber += BITS_FOR_PIXEL;
                }
            }

            var messageBytes = new byte[messageBits.Count];
            messageBits.CopyTo(messageBytes, 0);

            return new ImageFile(hiddenMessageContainer, messageBytes);
        }

        private bool IsOdd(byte byteValue)
        {
            return byteValue % 2 == 1;
        }
    }
}
