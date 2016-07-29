using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils
{
    public class ImageFile
    {
        public Bitmap Bitmap;
        public byte[] Bytes;

        public ImageFile(Bitmap bitmap, byte[] bytes)
        {
            Bitmap = bitmap;
            Bytes = bytes;
        }
    }
}
