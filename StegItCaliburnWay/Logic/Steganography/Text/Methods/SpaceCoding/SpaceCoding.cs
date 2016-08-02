using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using StegItCaliburnWay;
using StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.SpaceCoding;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.Utils.ExtensionsMethods;

namespace StegIt.Text.StegoTools
{
    public class SpaceCoding : ITextCodingMethod
    {
        private readonly SpaceCodingValidator _spaceCodingValidator;
        private const char SPACE = ' ';

        public SpaceCoding(SpaceCodingValidator spaceCodingValidator)
        {
            _spaceCodingValidator = spaceCodingValidator;
        }

        public static ArrayList GetSpacesPosition(char[] message)
        {
            var tmpList = new ArrayList();

            var iterator = 0;

            foreach (var c in message)
            {
                if (c == SPACE)
                    tmpList.Add(iterator);

                iterator++;
            }

            return tmpList;
        }

        public byte[] CreateHiddenMessage(byte[] container, byte[] messageBytes)
        {
            var messageToHide = TextUtils.GetUTF8CharArrayFromByteStream(messageBytes);

            var containerText = TextUtils.GetUTF8CharArrayFromByteStream(container);

            _spaceCodingValidator.CheckIfCanHideMessageOrThrow(containerText, messageBytes);

            var fileToSaveBytes = Encoding.UTF8.GetBytes(messageToHide);

            var bitsFromFileToSave = TextUtils.GetMessageBitArray(fileToSaveBytes);

            var spacesIndex = GetSpacesPosition(containerText);

            var numberOfPositiveBits = bitsFromFileToSave.GetPositiveBitsCount();

            var outputFileLength = containerText.Length + numberOfPositiveBits;

            var hiddenMessage = new char[outputFileLength];

            var insertedSpaces = 0;
            var insertedHiddenBits = 0;

            for (var i = 0; i < containerText.Length; i++)
            {
                hiddenMessage[i + insertedSpaces] = containerText[i];

                if (spacesIndex.Contains(i) && insertedHiddenBits < bitsFromFileToSave.Length)
                {
                    if (bitsFromFileToSave.Get(insertedHiddenBits))
                    {
                        hiddenMessage[i + insertedSpaces + 1] = SPACE;
                        insertedSpaces++;
                    }
                    insertedHiddenBits++;
                }
            }

            return Encoding.UTF8.GetBytes(hiddenMessage);
        }

        public byte[] DecodeHiddenMessage(byte[] container)
        {
            var openedFile = TextUtils.GetUTF8CharArrayFromByteStream(container);

            var messageBits = new BitArray(openedFile.SpaceCount());

            var shouldReadOneBit = false;
            var numberOfBitToPut = 0;

            for (var i = 0; i < openedFile.Length; i++)
            {
                if (shouldReadOneBit)
                {
                    messageBits.Set(numberOfBitToPut, openedFile[i] == SPACE);
                    numberOfBitToPut++;
                    shouldReadOneBit = false;
                    continue;
                }

                shouldReadOneBit = openedFile[i] == SPACE;
            }

            return messageBits.ToByteArray();
        }
    }
}
