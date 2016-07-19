using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace StegItCaliburnWay.Utils
{
    class ImageUtils
    {
        public static BitmapImage GetBitMapFromByteStream(byte[] bytes)
        {
            if (bytes != null)
            {
                using (var ms = new System.IO.MemoryStream(bytes))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }

            return new BitmapImage();
        }
    }
}
