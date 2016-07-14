using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace StegIt.Text.StegoTools
{
    class SpaceCoding : ITextCodingMethod
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

        public char[] CreateHiddenMessage(char[] openedFile, char[] message)
        {
            var fileToSaveBytes = Encoding.UTF8.GetBytes(message);

            var bitsFromFileToSave = TextUtils.GetMessageBitArray(fileToSaveBytes);

            if (GetSpacesCount(openedFile) < bitsFromFileToSave.Length)
            {
                throw new ArgumentException();
            }

            var spacesIndex = GetSpacesPosition(openedFile);

            var numberOfPositiveBits = bitsFromFileToSave.Cast<object>().Count(bit => bit.Equals(true));

            var outputFileLength = openedFile.Length + numberOfPositiveBits;

            var messageChars = new char[outputFileLength];

            var insertedSpaces = 0;
            var insertedHiddenBits = 0;

            for (var i = 0; i < openedFile.Length; i++)
            {
                messageChars[i + insertedSpaces] = openedFile[i];

                if (spacesIndex.Contains(i) && insertedHiddenBits < bitsFromFileToSave.Length)
                {
                    if (bitsFromFileToSave.Get(insertedHiddenBits))
                    {
                        messageChars[i + insertedSpaces + 1] = SPACE;
                        insertedSpaces++;
                    }
                    insertedHiddenBits++;
                }
            }

            return messageChars;
        }

        public char[] DecodeHiddenMessage(char[] openedFile)
        {
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

            var messageBytes = new byte[messageBits.Count];
            messageBits.CopyTo(messageBytes, 0);

            return Encoding.UTF8.GetString(messageBytes).ToCharArray();
        }
    }
}
