using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public override string Name
        {
            get { return "Bitmap - 24"; }
        }


        public override byte[] PerformHiding(ImageViewModel imageViewModel)
        {
            return null;
        }

        public override byte[] PerformDecoding(ImageViewModel imageViewModel)
        {
            return null;
        }
    }

    public class BitMap16 : ImageMethod
    {

        public override string Name
        {
            get { return "Bitmap - 16"; }
        }


        public override byte[] PerformHiding(ImageViewModel imageViewModel)
        {
            return null;
        }

        public override byte[] PerformDecoding(ImageViewModel imageViewModel)
        {
            return null;
        }
    }

    public class Gif : ImageMethod
    {

        public override string Name
        {
            get { return "GIF"; }
        }


        public override byte[] PerformHiding(ImageViewModel imageViewModel)
        {
            return null;
        }

        public override byte[] PerformDecoding(ImageViewModel imageViewModel)
        {
            return null;
        }
    }
}