using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils.Video
{
    public class VideoFile
    {
        public int FrameHeight { get; set; }
        public int FrameWidth { get; set; }
        public int FrameCount { get; set; }
        public uint FrameRate { get; set; }

        public byte[] hiddenMessageBytes;
        public readonly string FileName;

        public VideoFile(string fileName)
        {
            FileName = fileName;
            hiddenMessageBytes = TextUtils.GetBytesFromMessage(fileName.ToCharArray()); 
        }
    }
}
