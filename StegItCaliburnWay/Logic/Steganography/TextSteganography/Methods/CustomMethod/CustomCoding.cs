using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegIt.Text.StegoTools;
using StegItCaliburnWay.Utils;

namespace StegItCaliburnWay.Logic.Steganography.TextSteganography.Methods.CustomMethod
{
    public class CustomCoding : ITextCodingMethod
    {
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

            var numberOfPositiveBitsInMessage = messageBitArray.Cast<object>().Count(bit => bit.Equals(true));
            var numberOfPositiveBitsInSettingsHeader = settingsBitArray.Cast<object>().Count(bit => bit.Equals(true));

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
                            hiddenMessage.SetValue(settings.Coding, i + insertedHiddenBits);
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

            return null;
        }
    }
}
