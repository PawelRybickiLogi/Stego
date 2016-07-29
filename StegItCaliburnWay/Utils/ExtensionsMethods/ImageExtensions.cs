using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils.ExtensionsMethods
{
    public static class ImageExtensions
    {
        public static bool HasAlphaChannel(this Image img)
        {
            return img.PixelFormat == PixelFormat.Format32bppArgb;
        }

        public static bool IsIndextedImage(this Image img)
        {
            return img.PixelFormat == PixelFormat.Format8bppIndexed;
        }

        public static byte BitsForPixel(this Image img)
        {
            return (byte) (Bitmap.GetPixelFormatSize(img.PixelFormat) / 8);
        }

        public static void SaveAsNot32BitImage(this Image img, string fname)
        {
            var bmp = new Bitmap(img.Width, img.Height, img.PixelFormat);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));

            ImageConverter imgCon = new ImageConverter();
            var bytes = (byte[])imgCon.ConvertTo(bmp, typeof(byte[]));
            File.WriteAllBytes(fname, bytes);
        }

    }
}
