using System;
using System.Drawing;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Bitmap24
{
    public class Bitmap24Coding : ImageCodingMethod
    {
        private readonly Bitmap24Validator _bitmap24Validator;

        public Bitmap24Coding(Bitmap24Validator bitmap24Validator)
        {
            _bitmap24Validator = bitmap24Validator;
        }

        public byte[] CreateHiddenMessage(Bitmap container, byte[] message)
        {
            //var containerBitmap = new CustomBitmap(container);
            var bitsFromFileToSave = TextUtils.GetMessageBitArray(message);

            //_bitmap24Validator.CheckIfCanHideMessageOrThrow(containerBitmap, bitsFromFileToSave);

            var pixelsToEdit = bitsFromFileToSave.Length / 3;
            var additionalBits = bitsFromFileToSave.Length % 3;
            var pixelsEdited = 0;

            for (int i = 0; i < container.Height; i++)
            {
                if (pixelsEdited >= pixelsToEdit)
                    break;

                for (int j = 0; j < container.Width; j++)
                {
                    if (pixelsEdited < pixelsToEdit)
                    {
                        var pixelColor = container.GetPixel(i, j);

                        var pixelColorAfterHidingBits = pixelColor.ReplaceColorPixel(
                            bitsFromFileToSave.Get(0 + pixelsEdited*3),     //RED
                            bitsFromFileToSave.Get(1 + pixelsEdited*3),     //GREEN
                            bitsFromFileToSave.Get(2 + pixelsEdited*3));    //BLUE

                        container.SetPixel(i, j, pixelColorAfterHidingBits);

                        pixelsEdited++;
                    }
                    else
                    {
                        if (additionalBits == 1)
                        {
                            var pixelColor = container.GetPixel(i, j);
                            var pixelColorAfterChangingOneBit = 
                                pixelColor.Replace1ColorBit(bitsFromFileToSave.Get(bitsFromFileToSave.Length - 1));

                            container.SetPixel(i, j, pixelColorAfterChangingOneBit);

                            break;
                        }
                        else if (additionalBits == 2)
                        {
                            var pixelColor = container.GetPixel(i, j);
                            var pixelColorAfterChangingTwoBits =
                                pixelColor.Replace2ColorBits(bitsFromFileToSave.Get(bitsFromFileToSave.Length - 2), bitsFromFileToSave.Get(bitsFromFileToSave.Length - 1));

                            container.SetPixel(i, j, pixelColorAfterChangingTwoBits);
                        }
                    }
                }
            }

            return ImageUtils.BitmapToBytes(container);

        }

        public byte[] DecodeHiddenMessage(byte[] openedFile)
        {
            throw new NotImplementedException();
        }
    }
}
