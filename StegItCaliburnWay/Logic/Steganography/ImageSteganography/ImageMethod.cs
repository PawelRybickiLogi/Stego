using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Bitmap24;
using StegItCaliburnWay.Logic.Steganography.ImageSteganography.Methods.Gif;
using StegItCaliburnWay.ViewModels;

namespace StegItCaliburnWay.Logic.Steganography.ImageSteganography
{
    public abstract class ImageMethod
    {
        public abstract string Name { get; }
        public abstract byte[] PerformDecoding(ImageViewModel imageViewModel);
        public abstract byte[] PerformHiding(ImageViewModel imageViewModel);
    }

    public class BitMap24 : ImageMethod
    {
        private readonly Bitmap24Coding _bitmap24Coding;

        public BitMap24(Bitmap24Coding bitmap24Coding)
        {
            _bitmap24Coding = bitmap24Coding;
        }

        public override string Name
        {
            get { return "Bitmap - 24"; }
        }


        public override byte[] PerformHiding(ImageViewModel imageViewModel)
        {
            return _bitmap24Coding.CreateHiddenMessage(imageViewModel.ContainerBitmapMessage, imageViewModel.MessageToHide);
        }

        public override byte[] PerformDecoding(ImageViewModel imageViewModel)
        {
            return _bitmap24Coding.DecodeHiddenMessage(imageViewModel.ContainerRawMessage);
        }
    }

    public class BitMap16 : ImageMethod
    {
        private readonly Bitmap16Coding _bitmap16Coding;

        public BitMap16(Bitmap16Coding bitmap16Coding)
        {
            _bitmap16Coding = bitmap16Coding;
        }

        public override string Name
        {
            get { return "Bitmap - 16"; }
        }


        public override byte[] PerformHiding(ImageViewModel imageViewModel)
        {
            return _bitmap16Coding.CreateHiddenMessage(imageViewModel.ContainerBitmapMessage, imageViewModel.MessageToHide);
        }

        public override byte[] PerformDecoding(ImageViewModel imageViewModel)
        {
            return _bitmap16Coding.DecodeHiddenMessage(imageViewModel.ContainerRawMessage);
        }
    }

    public class Gif : ImageMethod
    {
        private readonly GifCoding _gifCoding;

        public Gif(GifCoding gifCoding)
        {
            _gifCoding = gifCoding;
        }

        public override string Name
        {
            get { return "GIF"; }
        }


        public override byte[] PerformHiding(ImageViewModel imageViewModel)
        {
            return _gifCoding.CreateHiddenMessage(imageViewModel.ContainerBitmapMessage, imageViewModel.MessageToHide);
        }

        public override byte[] PerformDecoding(ImageViewModel imageViewModel)
        {
            return _gifCoding.DecodeHiddenMessage(imageViewModel.ContainerRawMessage);
        }
    }
}