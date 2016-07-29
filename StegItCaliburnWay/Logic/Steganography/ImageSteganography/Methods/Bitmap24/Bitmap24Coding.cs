using System;
using System.Collections;
using System.Drawing;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Bitmap24
{
    public class Bitmap24Coding : ImageCodingMethod
    {
        private byte bitsForPixel = 3;

        private readonly Bitmap24Validator _bitmap24Validator;

        public Bitmap24Coding(Bitmap24Validator bitmap24Validator)
        {
            _bitmap24Validator = bitmap24Validator;
        }

        public ImageFile CreateHiddenMessage(Bitmap container, byte[] message)
        {
            bitsForPixel = 3;

            var bitsFromMessageToSave = TextUtils.GetMessageBitArray(message);

            _bitmap24Validator.CheckIfCanHideMessageOrThrow(container, bitsFromMessageToSave);

            var containerDeepCopy = container.Clone(new Rectangle(0, 0, container.Width, container.Height), container.PixelFormat);

            var pixelsToEdit = bitsFromMessageToSave.Length / bitsForPixel;
            var additionalBits = bitsFromMessageToSave.Length % bitsForPixel;
            var pixelsEdited = 0;

            for (int i = 0; i < containerDeepCopy.Height; i++)
            {
                if (pixelsEdited >= pixelsToEdit)
                    break;

                for (int j = 0; j < containerDeepCopy.Width; j++)
                {
                    if (pixelsEdited < pixelsToEdit)
                    {
                        var pixelColor = containerDeepCopy.GetPixel(j, i);

                        var pixelColorAfterHidingBits = pixelColor.ReplaceColorPixel(
                            bitsFromMessageToSave.Get(0 + pixelsEdited * bitsForPixel),     //RED
                            bitsFromMessageToSave.Get(1 + pixelsEdited * bitsForPixel),     //GREEN
                            bitsFromMessageToSave.Get(2 + pixelsEdited * bitsForPixel));    //BLUE

                        containerDeepCopy.SetPixel(j, i, pixelColorAfterHidingBits);

                        pixelsEdited++;
                    }
                    else
                    {
                        if (additionalBits == 1)
                        {
                            var pixelColor = containerDeepCopy.GetPixel(j, i);
                            var pixelColorAfterChangingOneBit = 
                                pixelColor.Replace1ColorBit(bitsFromMessageToSave.Get(bitsFromMessageToSave.Length - 1));

                            containerDeepCopy.SetPixel(j, i, pixelColorAfterChangingOneBit);

                            break;
                        }
                        if (additionalBits == 2)
                        {
                            var pixelColor = containerDeepCopy.GetPixel(j, i);
                            var pixelColorAfterChangingTwoBits =
                                pixelColor.Replace2ColorBits(bitsFromMessageToSave.Get(bitsFromMessageToSave.Length - 2), bitsFromMessageToSave.Get(bitsFromMessageToSave.Length - 1));

                            containerDeepCopy.SetPixel(j, i, pixelColorAfterChangingTwoBits);
                        }
                    }
                }
            }

            return new ImageFile(containerDeepCopy, ImageUtils.BitmapToBytes(containerDeepCopy));
        }

        public ImageFile DecodeHiddenMessage(Bitmap hiddenMessageContainer)
        {
            var messageBits = new BitArray(hiddenMessageContainer.Width * hiddenMessageContainer.Height * bitsForPixel);

            var bitInputNumber = 0;

            for (int i = 0; i < hiddenMessageContainer.Height; i++)
            {
                for (int j = 0; j < hiddenMessageContainer.Width; j++)
                {
                    var pixelColor = hiddenMessageContainer.GetPixel(j, i);
                    messageBits.Set(bitInputNumber, IsOdd(pixelColor.R));
                    messageBits.Set(bitInputNumber + 1, IsOdd(pixelColor.G));
                    messageBits.Set(bitInputNumber + 2, IsOdd(pixelColor.B));

                    bitInputNumber += bitsForPixel;
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
