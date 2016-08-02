using StegItCaliburnWay.Logic.Steganography.AudioSteganography.Methods.Wave;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.ViewModels;

namespace StegItCaliburnWay.Logic.Steganography.AudioSteganography
{
    public abstract class AudioMethod
    {
        public abstract string Name { get; }
        public abstract Type dialogType { get; }
        public abstract AudioFile PerformDecoding(AudioViewModel soundViewModel);
        public abstract AudioFile PerformHiding(AudioViewModel soundViewModel);

        private AudioFile _containerAudioMessage;
        private AudioFile _hiddenAudioMessage;

        public class WaveCodingMethod : AudioMethod
        {
            private readonly WaveCoding _waveCoding;

            public WaveCodingMethod(WaveCoding waveCoding)
            {
                _waveCoding = waveCoding;
            }

            public override string Name
            {
                get { return "Wave (8/16/24 bit/sample)"; }
            }

            public override Type dialogType
            {
                get { return DialogType.Audio; }
            }

            public override AudioFile PerformHiding(AudioViewModel soundViewModel)
            {
                return _waveCoding.CreateHiddenMessage(soundViewModel.ContainerAudioFile, soundViewModel.MessageToHide);
            }

            public override AudioFile PerformDecoding(AudioViewModel soundViewModel)
            {
                return _waveCoding.DecodeHiddenMessage(soundViewModel.ContainerAudioFile);
            }
        }
    }
}
