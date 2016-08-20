using System.Collections;
using System.Drawing;
using StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods;
using StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Bitmap32;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.Utils.ExtensionsMethods;

namespace StegItCaliburnWay.Logic.Steganography.Image.Methods.Bitmap32
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
