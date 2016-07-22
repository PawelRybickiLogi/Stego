using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils
{
    public class ImageFilePicked
    {
        public Bitmap Bitmap;
        public byte[] Bytes;

        public ImageFilePicked(Bitmap bitmap, byte[] bytes)
        {
            Bitmap = bitmap;
            Bytes = bytes;
        }
    }
}
