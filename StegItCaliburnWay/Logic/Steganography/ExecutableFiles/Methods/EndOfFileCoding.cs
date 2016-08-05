using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Utils.ExtensionsMethods;

namespace StegItCaliburnWay.Logic.Steganography.ExecutableFiles.Methods
{
    public class EndOfFileCoding : ExecutableCodingMethod
    {
        public byte[] CreateHiddenMessage(byte[] container, byte[] message)
        {
            var messageLength = message.Length;

            var messageLengthAsByteArray = messageLength.GetBitsAs4ByteArray();

            byte[] hiddenMessage = new byte[container.Length + message.Length + 4];

            //copy original bytes
            for (int i = 0; i < container.Length; i++)
            {
                hiddenMessage[i] = container[i];
            }

            //add hidden info
            for (int i = 0; i < message.Length; i++)
            {
                hiddenMessage[i + container.Length] = message[i];
            }

            for (int i = 0; i < messageLengthAsByteArray.Length; i++)
            {
                hiddenMessage[i + container.Length + message.Length] = messageLengthAsByteArray[i];
            }

            return hiddenMessage;
        }

        public byte[] DecodeHiddenMessage(byte[] hiddenMessageContainer)
        {
            var hiddenMessageLengthByteArray = new byte[4];

            var bitInputCount = 0;
            for (int i = hiddenMessageContainer.Length - 4; i < hiddenMessageContainer.Length; i++)
            {
                hiddenMessageLengthByteArray[bitInputCount] = hiddenMessageContainer[i];
                bitInputCount++;
            }

            var hiddenMessageLength = hiddenMessageLengthByteArray.GetIntFromByteArray();

            if(hiddenMessageLength == 0)
                throw new Exception("Plik nie zawiera ukrytej wiadomości!");

            var hiddenMessageContent = new byte[hiddenMessageLength];

            for (int i = 0; i < hiddenMessageLength; i++)
            {
                hiddenMessageContent[i] =
                    hiddenMessageContainer[hiddenMessageContainer.Length - hiddenMessageLength - hiddenMessageLengthByteArray.Length + i];
            }

            return hiddenMessageContent;
        }
    }
}
