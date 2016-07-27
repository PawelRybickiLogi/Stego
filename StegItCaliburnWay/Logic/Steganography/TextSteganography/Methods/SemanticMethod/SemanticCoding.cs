using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegIt.Text.StegoTools.SemanticMethod;
using StegItCaliburnWay;

namespace StegIt.Text.StegoTools
{
    public class SemanticCoding : ITextCodingMethod
    {
        private readonly Dictionary<char, char> listOfLettersThatCanBeChanged = SemanticLettersValues.GetLetters();

        public byte[] CreateHiddenMessage(byte[] container, byte[] message)
        {
            var openedFile = TextUtils.GetUTF8CharArrayFromByteStream(container);

            var fileToSaveBytes = message;

            var bitsFromFileToSave = TextUtils.GetMessageBitArray(fileToSaveBytes);

            if (openedFile.Length < bitsFromFileToSave.Length)
            {
                throw new ArgumentException();
            }

            var messageChars = new char[openedFile.Length];
            var insertedHiddenBits = 0;

            for (var i = 0; i < openedFile.Length; i++)
            {
                if (listOfLettersThatCanBeChanged.ContainsKey(openedFile[i]) && insertedHiddenBits < bitsFromFileToSave.Length)
                {
                    if (bitsFromFileToSave.Get(insertedHiddenBits))
                    {
                        messageChars[i] = listOfLettersThatCanBeChanged[openedFile[i]];
                    }
                    else
                    {
                        messageChars[i] = openedFile[i];
                    }
                    insertedHiddenBits++;
                }
                else
                {
                    messageChars[i] = openedFile[i];
                }
            }

            return TextUtils.GetBytesFromMessage(messageChars);
        }

        public byte[] DecodeHiddenMessage(byte[] container)
        {
            var openedFile = TextUtils.GetUTF8CharArrayFromByteStream(container);

            var numberOfBitsHidden = GetNumberOfLettersThatCouldBeHidden(openedFile);
            var messageBits = new BitArray(numberOfBitsHidden);

            var bitInputNumber = 0;

            foreach (char letter in openedFile)
            {
                if (listOfLettersThatCanBeChanged.ContainsKey(letter) || listOfLettersThatCanBeChanged.ContainsValue(letter))
                {
                    messageBits.Set(bitInputNumber, listOfLettersThatCanBeChanged.ContainsValue(letter));
                    bitInputNumber++;
                }
            }

            var messageBytes = new byte[messageBits.Count];
            messageBits.CopyTo(messageBytes, 0);

            return messageBytes;
        }

        private int GetNumberOfLettersThatCouldBeHidden(char[] openedFile)
        {
            return openedFile.Count(c => listOfLettersThatCanBeChanged.ContainsKey(c) || listOfLettersThatCanBeChanged.ContainsValue(c));
        }
    }


    internal class Letter
    {
        public char originalLetter;
        public char modifiedLetter;

        public Letter(char originalLetter, char modifiedLetter)
        {
            this.originalLetter = originalLetter;
            this.modifiedLetter = modifiedLetter;
        }
    }

}
