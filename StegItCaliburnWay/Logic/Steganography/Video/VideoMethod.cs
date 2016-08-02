
using System;
using StegItCaliburnWay.Logic.Steganography.Video.Methods.Avi;
using StegItCaliburnWay.Utils;
using StegItCaliburnWay.Utils.Video;
using StegItCaliburnWay.ViewModels;
using Type = StegItCaliburnWay.Utils.Type;


namespace StegItCaliburnWay.Logic.Steganography.Video
{
    public abstract class VideoMethod
    {
        public abstract string Name { get; }
        public abstract Type dialogType { get; }
        public abstract VideoFile PerformDecoding(VideoViewModel videoViewModel);
        public abstract VideoFile PerformHiding(VideoViewModel videoViewModel);
    }

    public class AviCodingMethod : VideoMethod
    {
        private readonly AviCoding _aviCoding;

        public AviCodingMethod(AviCoding aviCoding)
        {
            _aviCoding = aviCoding;
        }

        public override string Name
        {
            get { return "Avi"; }
        }

        public override Type dialogType
        {
            get { return DialogType.PngImage; }
        }

        public override VideoFile PerformDecoding(VideoViewModel videoViewModel)
        {
            return _aviCoding.DecodeHiddenMessage();
        }

        public override VideoFile PerformHiding(VideoViewModel videoViewModel)
        {
            return _aviCoding.CreateHiddenMessage();
        }
    }
}
