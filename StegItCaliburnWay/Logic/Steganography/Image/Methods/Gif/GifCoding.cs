using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using StegItCaliburnWay.Properties;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.Utils.ExtensionsMethods;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Gif
{
    public class GifCoding : ImageCodingMethod
    {
        private readonly GifCodingValidator _gifCodingValidator;

        public GifCoding(GifCodingValidator gifCodingValidator)
        {
            _gifCodingValidator = gifCodingValidator;
        }

        public ImageFile CreateHiddenMessage(Bitmap container, byte[] message)
        {
            var bitsFromMessageToSave = TextUtils.GetMessageBitArray(message);

            _gifCodingValidator.CheckIfCanHideMessageOrThrow(container, bitsFromMessageToSave);

            var containerDeepCopy = container.Clone(new Rectangle(0, 0, container.Width, container.Height), container.PixelFormat);

            Dictionary<byte, Color> originalPalleteColorForIndex = new Dictionary<byte, Color>();

            for (int i = 0; i < container.Palette.Entries.Length; i++)
            {
                originalPalleteColorForIndex.Add((byte)i, container.Palette.Entries[i]);
            }

            BitmapData bitmapData = containerDeepCopy.LockBits(new Rectangle(0, 0, containerDeepCopy.Width, container.Height),
                                                ImageLockMode.ReadWrite, containerDeepCopy.PixelFormat);

            var mostUsedColorsInPalette = bitmapData.GetAllPixelsIndexes()
                .GroupBy(x => x)
                .OrderByDescending(g => g.Count())
                .Select(c => c.FirstOrDefault())
                .Take(container.Palette.Entries.Length / 2)
                .ToList();

            ColorPalette paletteCopy = containerDeepCopy.Palette;

            Dictionary<byte, byte> oldToNewIndexMap = new Dictionary<byte, byte>();

            for (int i = 0; i < container.Palette.Entries.Length / 2; i++)
            {
                paletteCopy.Entries[i] = containerDeepCopy.Palette.Entries[mostUsedColorsInPalette[i]];
                paletteCopy.Entries[i + container.Palette.Entries.Length / 2] = containerDeepCopy.Palette.Entries[mostUsedColorsInPalette[i]];
                oldToNewIndexMap.Add(mostUsedColorsInPalette[i], (byte) i);
            }

            var firstHalfFromPalette = paletteCopy.Entries.Take(paletteCopy.Entries.Length / 2).ToList();

            bitmapData.MovePixelsToFirstHalfOfThePalette(oldToNewIndexMap, firstHalfFromPalette, originalPalleteColorForIndex);

            containerDeepCopy.Palette = paletteCopy;

            var hiddenBits = 0;

            for (int i = 0; i < bitmapData.Width; i++)
            {
                if (hiddenBits >= bitsFromMessageToSave.Length)
                    break;

                for (int j = 0; j < bitmapData.Height; j++)
                {
                    if (hiddenBits >= bitsFromMessageToSave.Length)
                        break;

                    if(bitsFromMessageToSave[hiddenBits])
                        bitmapData.SetPixel(i, j, (byte) (bitmapData.GetPixel(i, j) + containerDeepCopy.Palette.Entries.Length / 2));

                    hiddenBits++;
                }
            }

            containerDeepCopy.UnlockBits(bitmapData);

            return new ImageFile(containerDeepCopy, ImageUtils.GifToBytes(containerDeepCopy));
        }

        public ImageFile DecodeHiddenMessage(Bitmap hiddenMessageContainer)
        {
            BitmapData bitmapData = hiddenMessageContainer.LockBits(new Rectangle(0, 0, hiddenMessageContainer.Width, hiddenMessageContainer.Height),
                                    ImageLockMode.ReadWrite, hiddenMessageContainer.PixelFormat);

            var messageBits = new BitArray(hiddenMessageContainer.Width * hiddenMessageContainer.Height);

            var hiddenBitIndex = 0;

            for (int i = 0; i < bitmapData.Width; i++)
            {
                for (int j = 0; j < bitmapData.Height; j++)
                {
                    messageBits.Set(hiddenBitIndex, bitmapData.GetPixel(i, j) >= hiddenMessageContainer.Palette.Entries.Length / 2);
                    hiddenBitIndex++;
                }
            }

            hiddenMessageContainer.UnlockBits(bitmapData);

            var messageBytes = new byte[messageBits.Count];
            messageBits.CopyTo(messageBytes, 0);

            return new ImageFile(hiddenMessageContainer, messageBytes);
        }
    }
}
