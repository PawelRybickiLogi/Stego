using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegItCaliburnWay.Utils.Video;

namespace StegItCaliburnWay.Logic.Steganography.Video.Methods
{
    interface VideoCodingMethod
    {
        VideoFile CreateHiddenMessage();
        VideoFile DecodeHiddenMessage();
    }
}
