using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StegItCaliburnWay.Utils.Video
{
    public class AviFileWriting
    {
        private int aviFile = 0;
        private IntPtr aviStream = IntPtr.Zero;
        private UInt32 frameRate = 0;
        private int countFrames = 0;
        private int width = 0;
        private int height = 0;
        private UInt32 stride = 0;
        private UInt32 fccType = AviReadingMethods.StreamtypeVIDEO; // vids
        private UInt32 fccHandler = 1668707181; 

        public void Open(string fileName, UInt32 frameRate)
        {
            this.frameRate = frameRate;

            AviReadingMethods.AVIFileInit();

            int hr = AviReadingMethods.AVIFileOpen(
                ref aviFile, fileName,
                4097, 0);
            if (hr != 0)
            {
                throw new Exception("Problem podczas otwierania pliku AVI" + hr.ToString());
            }
        }

        public void AddFrame(Bitmap bmp)
        {

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

            BitmapData bmpDat = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            if (countFrames == 0)
            {
                this.stride = (UInt32)bmpDat.Stride;
                this.width = bmp.Width;
                this.height = bmp.Height;
                CreateStream();
            }

            int result = AviReadingMethods.AVIStreamWrite(aviStream,
                countFrames, 1,
                bmpDat.Scan0, 
                (Int32)(stride * height),
                0, 0, 0);

            if (result != 0)
            {
                throw new Exception("Problem podczas otwierania pliku AVI" + result.ToString());
            }

            bmp.UnlockBits(bmpDat);
            countFrames++;
        }

        public void Close()
        {
            if (aviStream != IntPtr.Zero)
            {
                AviReadingMethods.AVIStreamRelease(aviStream);
                aviStream = IntPtr.Zero;
            }
            if (aviFile != 0)
            {
                AviReadingMethods.AVIFileRelease(aviFile);
                aviFile = 0;
            }
            AviReadingMethods.AVIFileExit();
        }

        private void CreateStream()
        {
            AviReadingMethods.AVISTREAMINFO strhdr = new AviReadingMethods.AVISTREAMINFO();
            strhdr.fccType = fccType;
            strhdr.fccHandler = fccHandler;
            strhdr.dwScale = 1;
            strhdr.dwRate = frameRate;
            strhdr.dwSuggestedBufferSize = (UInt32)(height * stride);
            strhdr.dwQuality = 10000; 
            strhdr.rcFrame.bottom = (UInt32)height;
            strhdr.rcFrame.right = (UInt32)width;
            strhdr.szName = new UInt16[64];

            int result = AviReadingMethods.AVIFileCreateStream(aviFile, out aviStream, ref strhdr);
            if (result != 0) { throw new Exception("Problem podczas tworzenia streamu pliku AVI" + result.ToString()); }

            AviReadingMethods.BITMAPINFOHEADER bi = new AviReadingMethods.BITMAPINFOHEADER();
            bi.biSize = (UInt32)Marshal.SizeOf(bi);
            bi.biWidth = (Int32)width;
            bi.biHeight = (Int32)height;
            bi.biPlanes = 1;
            bi.biBitCount = 24;
            bi.biSizeImage = (UInt32)(stride * height);

            result = AviReadingMethods.AVIStreamSetFormat(aviStream, 0, ref bi, Marshal.SizeOf(bi));
            if (result != 0) { throw new Exception("Problem podczas tworzenia streamu pliku AVI" + result.ToString()); }
        }
    }
}
