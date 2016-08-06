using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegIt.Text.StegoTools;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.Utils.ExtensionsMethods;

namespace StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod
{
    public class CustomCoding : ITextCodingMethod
    {
        private const int HEADER_FRAME_MIN_SIZE = 64;

        private readonly CustomCodingValidator _customCodingValidator;

        public CustomCoding(CustomCodingValidator customCodingValidator)
        {
            _customCodingValidator = customCodingValidator;
        }

        public byte[] CreateHiddenMessage(byte[] container, byte[] message)
        {
            _customCodingValidator.CheckIfCanHideMessageOrThrow(container, message);

            var settings = SettingsFrameFromContent.GetMaxCapacitySettingsFrameForMessage(message);

            var settingsBitArray = settings.ToBitArray();
            var messageBitArray = TextUtils.GetMessageBitArray(message);

            var numberOfPositiveBitsInMessage = messageBitArray.GetPositiveBitsCount();
            var numberOfPositiveBitsInSettingsHeader = settingsBitArray.GetPositiveBitsCount();

            var hiddenMessage = new char[container.Length + numberOfPositiveBitsInMessage + numberOfPositiveBitsInSettingsHeader];

            var insertedHiddenBits = 0;

            for (int i = 0; i < container.Length; i++)
            {
                if (i < settingsBitArray.Length + messageBitArray.Length)
                {
                    //Hide header
                    if (i < settingsBitArray.Length)
                    {
                        if (settingsBitArray.Get(i))
                        {
                            hiddenMessage.SetValue(CodingSign.SPACE, i + insertedHiddenBits);
                            hiddenMessage.SetValue(container[i], i + insertedHiddenBits + 1);
                            insertedHiddenBits++;
                        }
                        else
                        {
                            hiddenMessage.SetValue(container[i], i + insertedHiddenBits);
                        }
                    }
                    //Hide message
                    else
                    {
                        if (messageBitArray.Get(i - settingsBitArray.Length))
                        {
                            hiddenMessage.SetValue(settings.Coding, i + insertedHiddenBits);
                            hiddenMessage.SetValue(container[i], i + insertedHiddenBits + 1);
                            insertedHiddenBits++;
                        }
                        else
                        {
                            hiddenMessage.SetValue(container[i], i + insertedHiddenBits);
                        }
                    }
                }
                else
                {
                    hiddenMessage.SetValue(container[i], i + insertedHiddenBits);
                }

            }

            return Encoding.UTF8.GetBytes(hiddenMessage);
        }

        public byte[] DecodeHiddenMessage(byte[] container)
        {
            _customCodingValidator.CheckIfCanDecodeMessageOrThrow(container);

            var settings = new SettingsFrameFromContent(container);

            var headerPositiveBits = settings.ToBitArray().GetPositiveBitsCount();
            var headerSize = headerPositiveBits + HEADER_FRAME_MIN_SIZE;

            var hiddenMessageBits = new BitArray(settings.MessageLength);
            var decodedHiddenBits = 0;
            var hiddenBitCounter = 0;

            var containerChars = TextUtils.GetUTF8CharArrayFromByteStream(container);

            for (int i = headerSize + settings.Shift; i < headerSize + settings.MessageLength; i+= settings.JumpValue + 1)
            {
                if (hiddenBitCounter < hiddenMessageBits.Length)
                {
                    var let = containerChars[i + decodedHiddenBits];

                    if (containerChars[i + decodedHiddenBits] == settings.Coding)
                    {
                        hiddenMessageBits.Set(hiddenBitCounter, true);
                        decodedHiddenBits++;
                    }
                    else
                    {
                        hiddenMessageBits.Set(hiddenBitCounter, false);
                    }
                    hiddenBitCounter++;
                }
                else
                {
                    break;
                }
            }

            var arej = hiddenMessageBits.ToByteArray();

            return arej;
        }
    }
}
