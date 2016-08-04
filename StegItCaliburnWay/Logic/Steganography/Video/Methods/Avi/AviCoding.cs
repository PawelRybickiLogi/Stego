using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using StegItCaliburnWay.Utils.ExtensionsMethods;
using StegItCaliburnWay.Utils.Video;

namespace StegItCaliburnWay.Logic.Steganography.Video.Methods.Avi
{
    public class AviCoding : VideoCodingMethod
    {
        private readonly AviFileReading _aviFileReading;
        private readonly AviFileWriting _aviFileWriting;
        private readonly AviCodingValidator _aviCodingValidator;

        private const byte bitsForPixel = 3;

        public AviCoding(
            AviFileReading aviFileReading,
            AviFileWriting aviFileWriting,
            AviCodingValidator aviCodingValidator)
        {
            _aviFileReading = aviFileReading;
            _aviFileWriting = aviFileWriting;
            _aviCodingValidator = aviCodingValidator;
        }

        public VideoFile CreateHiddenMessage(VideoFile containerVideoFile, byte[] messageToHide)
        {
            _aviCodingValidator.CheckIfCanHideMessageOrThrow(containerVideoFile, messageToHide);

            var singleFrameBitsCapacity = containerVideoFile.FrameHeight * containerVideoFile.FrameWidth;

            var bitsFromMessageToSave = TextUtils.GetMessageBitArray(messageToHide);

            var framesNeededToHideMessage = bitsFromMessageToSave.Length.DivideRoundingUp(singleFrameBitsCapacity);

            var bitmapsFromOriginalVideo = new List<Bitmap>();

            _aviFileReading.Open(containerVideoFile.FileName);

            for (int i = 0; i < containerVideoFile.FrameCount; i++)
            {
                var frameFromOriginalVideo = _aviFileReading.ExportBitmapFromFrameNumber(i);
                bitmapsFromOriginalVideo.Add(frameFromOriginalVideo);
            }

            _aviFileReading.Close();

            FileInfo hiddenMessageFile = new FileInfo(containerVideoFile.FileName);

            var pathToCopyHiddenFile = hiddenMessageFile.DirectoryName + "\\" + hiddenMessageFile.Name.Insert(hiddenMessageFile.Name.Length - 4, "_zakodowane");

            hiddenMessageFile.CopyTo(pathToCopyHiddenFile, true); 


            _aviFileWriting.Open(pathToCopyHiddenFile, containerVideoFile.FrameRate);

            var additionalBits = bitsFromMessageToSave.Length % bitsForPixel;
            var pixelsToEdit = bitsFromMessageToSave.Length / bitsForPixel;
            var pixelsEdited = 0;

            for (int i = 0; i < framesNeededToHideMessage; i++)
            {
                if (pixelsEdited >= pixelsToEdit)
                    break;

                var bitmapToChange = bitmapsFromOriginalVideo[i];
    
                for (int j = 0; j < bitmapToChange.Height; j++)
                {
                    if (pixelsEdited >= pixelsToEdit)
                        break;

                    for (int k = 0; k < bitmapToChange.Width; k++)
                    {
                        if (pixelsEdited < pixelsToEdit)
                        {

                            var pixelColor = bitmapToChange.GetPixel(k, j);

                            var pixelColorAfterHidingBits = pixelColor.ReplaceColorPixel(
                                bitsFromMessageToSave.Get(0 + pixelsEdited*bitsForPixel),   //RED
                                bitsFromMessageToSave.Get(1 + pixelsEdited*bitsForPixel),   //GREEN
                                bitsFromMessageToSave.Get(2 + pixelsEdited*bitsForPixel));  //BLUE

                            bitmapToChange.SetPixel(k, j, pixelColorAfterHidingBits);

                            pixelsEdited++;
                        }
                        else
                        {
                            if (additionalBits == 1)
                            {
                                var pixelColor = bitmapToChange.GetPixel(k, j);
                                var pixelColorAfterChangingOneBit =
                                    pixelColor.Replace1ColorBit(bitsFromMessageToSave.Get(bitsFromMessageToSave.Length - 1));

                                bitmapToChange.SetPixel(j, i, pixelColorAfterChangingOneBit);

                                break;
                            }
                            if (additionalBits == 2)
                            {
                                var pixelColor = bitmapToChange.GetPixel(k, j);
                                var pixelColorAfterChangingTwoBits =
                                    pixelColor.Replace2ColorBits(bitsFromMessageToSave.Get(bitsFromMessageToSave.Length - 2), bitsFromMessageToSave.Get(bitsFromMessageToSave.Length - 1));

                                bitmapToChange.SetPixel(k, j, pixelColorAfterChangingTwoBits);
                            }
                        }
                    }
                }

                _aviFileWriting.AddFrame(bitmapToChange);
            }

            //Add the rest of the frames
            for (int i = framesNeededToHideMessage; i < containerVideoFile.FrameCount; i++)
            {
                _aviFileWriting.AddFrame(bitmapsFromOriginalVideo[i]);
            }

            _aviFileWriting.Close();

            _aviFileReading.Open(pathToCopyHiddenFile);

            var videoFileWithHiddenData = new VideoFile(pathToCopyHiddenFile)
            {
                FrameHeight = _aviFileReading.Bih.biHeight,
                FrameWidth = _aviFileReading.Bih.biWidth,
                FrameCount = _aviFileReading.CountFrames,
                FrameRate = _aviFileReading.FrameRate
            };

            _aviFileReading.Close();

            return videoFileWithHiddenData;
        }

        public VideoFile DecodeHiddenMessage(VideoFile containerVideoFile)
        {
            var messageBits = new BitArray(containerVideoFile.FrameCount * containerVideoFile.FrameWidth * containerVideoFile.FrameHeight * bitsForPixel);

            var bitInputNumber = 0;

            _aviFileReading.Open(containerVideoFile.FileName);

            for (int p = 0; p < containerVideoFile.FrameCount; p++)
            {
                var frameFromOriginalVideo = _aviFileReading.ExportBitmapFromFrameNumber(p);

                for (int i = 0; i < containerVideoFile.FrameHeight; i++)
                {
                    for (int j = 0; j < containerVideoFile.FrameWidth; j++)
                    {
                        var pixelColor = frameFromOriginalVideo.GetPixel(j, i);
                        messageBits.Set(bitInputNumber, IsOdd(pixelColor.R));
                        messageBits.Set(bitInputNumber + 1, IsOdd(pixelColor.G));
                        messageBits.Set(bitInputNumber + 2, IsOdd(pixelColor.B));

                        bitInputNumber += bitsForPixel;
                    }
                }
            }

            _aviFileReading.Close();

            var messageBytes = new byte[messageBits.Count];
            messageBits.CopyTo(messageBytes, 0);

            return new VideoFile(containerVideoFile.FileName)
            {
                hiddenMessageBytes = messageBytes
            };
        }

        private bool IsOdd(byte byteValue)
        {
            return byteValue % 2 == 1;
        }
    }
}
