using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod
{
    public class CustomCodingValidator
    {
        private static int HEADER_SIZE = 64;
        public void CheckIfCanHideMessageOrThrow(byte[] container, byte[] message)
        {
            var settings = SettingsFrameFromContent.GetMaxCapacitySettingsFrameForMessage(message);

            var requiredSpaceToWriteMsg = settings.Shift + settings.MessageLength * (settings.JumpValue + 1);

/*            if (container.Length < HEADER_SIZE + requiredSpaceToWriteMsg)
                throw new Exception("Wiadomość jest zbyt długa aby umieścić ją w tekście" + Environment.NewLine +
                                    "Długość wiadomości w bitach: " + HEADER_SIZE + " (ramka ustawień), " + requiredSpaceToWriteMsg + " (wymagane miejsce na wiadomość)" + Environment.NewLine +
                                    "Ilośc dostępnego miejsca w tekście: " + container.Length);*/
        }

        public void CheckIfCanDecodeMessageOrThrow(byte[] container)
        {
            if (container.Length < 64)
                throw new Exception("Kontener jest zbyt krótki aby pomieścił ukrytą wiadomość," + Environment.NewLine +
                                    "Długość wiadomości (" + container.Length + ") nie pozwala nawet na umieszczenie nagłówka ramki");
        }
    }
}
