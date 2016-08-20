using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Utils.ExtensionsMethods;

namespace StegItCaliburnWay.Logic.Steganography.AudioSteganography.Methods.Wave
{
    public class WaveCoding : AudioCodingMethod
    {
        //In 8 bit sample, hide 2 bit of info
        //In 16 bit sample, hide 4 bit of info, etc...
        public static byte CHANGING_SAMPLES_FACTOR = 4;

        private readonly WaveCodingValidator _waveCodingValidator;

        public WaveCoding(WaveCodingValidator waveCodingValidator)
        {
            _waveCodingValidator = waveCodingValidator;
        }

        public AudioFile CreateHiddenMessage(AudioFile containerAudioFile, byte[] messageToHide)
        {
            _waveCodingValidator.CheckIfCanHideMessageOrThrow(containerAudioFile, messageToHide);

            var bitsFromMessageToSave = TextUtils.GetMessageBitArray(messageToHide);

            var bitsPerSampleThatCanBeHide = containerAudioFile.waveFile.bitsPerSample / CHANGING_SAMPLES_FACTOR;
            var hidingSteps = bitsFromMessageToSave.Length / bitsPerSampleThatCanBeHide;

            var samples = containerAudioFile.waveFile.samples;

            for (int i = 0; i < hidingSteps; i++)
            {
                var bitsToPut = bitsFromMessageToSave.GetBitArrayFromBitArrayRange((int)(i * bitsPerSampleThatCanBeHide), (int) bitsPerSampleThatCanBeHide);
                samples[i] = samples[i].PutBitsAsLSB(bitsToPut);
            }

            containerAudioFile.waveFile.Save();

            return new AudioFile(containerAudioFile.bytes);
        }

        public AudioFile DecodeHiddenMessage(AudioFile containerAudioFile)
        {
            var hiddenBitsPerSample = containerAudioFile.waveFile.bitsPerSample / CHANGING_SAMPLES_FACTOR;

            var hiddenMessageBits = new BitArray((int)(containerAudioFile.waveFile.samples.Length * hiddenBitsPerSample));

            var maxNumberOfDecodingSteps = containerAudioFile.waveFile.samples.Length;

            for (int i = 0; i < maxNumberOfDecodingSteps; i++)
            {
                var uintValue = containerAudioFile.waveFile.samples[i];
                var intFromBitsToRead = uintValue.GetLastNBitsIntValue((int) hiddenBitsPerSample);

                var hiddenIntBits = new BitArray(new[] { intFromBitsToRead }).GetBitArrayFromBitArrayRange(0, (int) hiddenBitsPerSample);

                for(int j = 0; j < hiddenIntBits.Length; j++)
                {
                    hiddenMessageBits.Set(i * (int) hiddenBitsPerSample + j, hiddenIntBits[j]);
                }
            }
            
            containerAudioFile.waveFile.Save();

            var messageBytes = new byte[hiddenMessageBits.Count];
            hiddenMessageBits.CopyTo(messageBytes, 0);

            return new AudioFile(containerAudioFile.bytes, messageBytes);
        }
    }
}
