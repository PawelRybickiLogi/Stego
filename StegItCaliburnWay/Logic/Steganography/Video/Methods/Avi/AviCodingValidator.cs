using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Utils.Video;

namespace StegItCaliburnWay.Logic.Steganography.Video.Methods.Avi
{
    public class AviCodingValidator
    {
        public void CheckIfCanHideMessageOrThrow(VideoFile containerVideoFile, byte[] messageToHide)
        {
            var bitsFromMessageToSave = TextUtils.GetMessageBitArray(messageToHide);

            var videoCapacity = containerVideoFile.FrameCount * containerVideoFile.FrameHeight * containerVideoFile.FrameWidth * 3;

            if (bitsFromMessageToSave.Length > videoCapacity)
                throw new Exception("Wiadomość jest zbyt długa aby umieścić ją w pliku .avi" + Environment.NewLine +
                                    "Długość wiadomości w bajtach: " + bitsFromMessageToSave.Length + Environment.NewLine +
                                    "Ilośc dostępnego miejsca w pliku wideo: " + videoCapacity);
        }
    }
}
