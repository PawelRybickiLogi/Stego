using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using StegItCaliburnWay;
using StegItCaliburnWay.Utils;

namespace StegIt.Text.StegoTools
{
    public class SpaceCoding : ITextCodingMethod
    {
        private const char SPACE = ' ';

        public static int GetSpacesCount(char[] message)
        {
            return message.Count(m => m == SPACE);
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
            var message = TextUtils.GetUTF8CharArrayFromByteStream(messageBytes);

            var openedFile = TextUtils.GetUTF8CharArrayFromByteStream(container);

            var fileToSaveBytes = Encoding.UTF8.GetBytes(message);

            var bitsFromFileToSave = TextUtils.GetMessageBitArray(fileToSaveBytes);

            if (GetSpacesCount(openedFile) < bitsFromFileToSave.Length)
            {
                throw new ArgumentException();
            }

            var spacesIndex = GetSpacesPosition(openedFile);

            var numberOfPositiveBits = bitsFromFileToSave.GetPositiveBitsCount();

            var outputFileLength = openedFile.Length + numberOfPositiveBits;

            var hiddenMessage = new char[outputFileLength];

            var insertedSpaces = 0;
            var insertedHiddenBits = 0;

            for (var i = 0; i < openedFile.Length; i++)
            {
                hiddenMessage[i + insertedSpaces] = openedFile[i];

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

            var spacesCount = GetSpacesCount(openedFile);

            var messageBits = new BitArray(spacesCount);

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
