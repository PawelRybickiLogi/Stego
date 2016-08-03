using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace StegItCaliburnWay.Utils.Video
{
    public class AviFileReading
    {
        private readonly AviReadingMethods _aviReadingMethods;

        //position of the first frame, count of frames in the stream
        private int firstFrame = 0;
        private int countFrames = 0;
        private int getFrameObject;

        //pointers
        private int aviFile = 0;
        private IntPtr aviStream;
        private AviReadingMethods.AVISTREAMINFO streamInfo;

        public AviFileReading(AviReadingMethods aviReadingMethods)
        {
            _aviReadingMethods = aviReadingMethods;
        }

        public void Open(string fileName)
        {
            //Intitialize AVI library
            AviReadingMethods.AVIFileInit();

            //Open the file
            int result = AviReadingMethods.AVIFileOpen(
                ref aviFile, fileName,
                AviReadingMethods.OF_SHARE_DENY_WRITE, 0);

            if (result != 0)
            {
                throw new Exception("Error during openning Avi file: " + result.ToString());
            }

            //Get the video stream
            result = AviReadingMethods.AVIFileGetStream(
                aviFile,
                out aviStream,
                AviReadingMethods.StreamtypeVIDEO, 0);

            if (result != 0)
            {
                throw new Exception("Error during openning Avi file: " + result.ToString());
            }

            firstFrame = AviReadingMethods.AVIStreamStart(aviStream.ToInt32());
            countFrames = AviReadingMethods.AVIStreamLength(aviStream.ToInt32());

            streamInfo = new AviReadingMethods.AVISTREAMINFO();
            result = AviReadingMethods.AVIStreamInfo(aviStream.ToInt32(), ref streamInfo, Marshal.SizeOf(streamInfo));

            if (result != 0)
            {
                throw new Exception("Error during openning Avi file: " + result.ToString());
            }

            //Open frames

            AviReadingMethods.BITMAPINFOHEADER bih = new AviReadingMethods.BITMAPINFOHEADER();
            bih.biBitCount = 24;
            bih.biClrImportant = 0;
            bih.biClrUsed = 0;
            bih.biCompression = 0; //BI_RGB;
            bih.biHeight = (Int32)streamInfo.rcFrame.bottom;
            bih.biWidth = (Int32)streamInfo.rcFrame.right;
            bih.biPlanes = 1;
            bih.biSize = (UInt32)Marshal.SizeOf(bih);
            bih.biXPelsPerMeter = 0;
            bih.biYPelsPerMeter = 0;

            getFrameObject = AviReadingMethods.AVIStreamGetFrameOpen(aviStream, ref bih); //force function to return 24bit DIBS
            if (getFrameObject == 0)
            {
                throw new Exception("Error during openning Avi file!");
            }
        }

        public void ExportBitmap(int position, String dstFileName)
        {
            if (position > countFrames)
            {
                throw new Exception("Invalid frame position");
            }

            int pDib = AviReadingMethods.AVIStreamGetFrame(getFrameObject, firstFrame + position);

            AviReadingMethods.BITMAPINFOHEADER bih = new AviReadingMethods.BITMAPINFOHEADER();
            bih = (AviReadingMethods.BITMAPINFOHEADER)Marshal.PtrToStructure(new IntPtr(pDib), bih.GetType());

            if (bih.biSizeImage < 1)
            {
                throw new Exception("Exception in AVIStreamGetFrame: Not bitmap decompressed.");
            }

            //Copy the image
            byte[] bitmapData = new byte[bih.biSizeImage];
            int address = pDib + Marshal.SizeOf(bih);
            for (int offset = 0; offset < bitmapData.Length; offset++)
            {
                bitmapData[offset] = Marshal.ReadByte(new IntPtr(address));
                address++;
            }

            //Copy bitmap info
            byte[] bitmapInfo = new byte[Marshal.SizeOf(bih)];
            IntPtr ptr;
            ptr = Marshal.AllocHGlobal(bitmapInfo.Length);
            Marshal.StructureToPtr(bih, ptr, false);
            address = ptr.ToInt32();
            for (int offset = 0; offset < bitmapInfo.Length; offset++)
            {
                bitmapInfo[offset] = Marshal.ReadByte(new IntPtr(address));
                address++;
            }

            //Create file header
            AviReadingMethods.BITMAPFILEHEADER bfh = new AviReadingMethods.BITMAPFILEHEADER();
            bfh.bfType = AviReadingMethods.BMP_MAGIC_COOKIE;
            bfh.bfSize = (Int32)(55 + bih.biSizeImage); 
            bfh.bfReserved1 = 0;
            bfh.bfReserved2 = 0;
            bfh.bfOffBits = Marshal.SizeOf(bih) + Marshal.SizeOf(bfh);

            //Create or overwrite the destination file
            FileStream fs = new FileStream(dstFileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            //Write header
            bw.Write(bfh.bfType);
            bw.Write(bfh.bfSize);
            bw.Write(bfh.bfReserved1);
            bw.Write(bfh.bfReserved2);
            bw.Write(bfh.bfOffBits);
            //Write bitmap info
            bw.Write(bitmapInfo);
            //Write bitmap data
            bw.Write(bitmapData);
            var bmp = new Bitmap(fs);
            bmp.Save(dstFileName+"22");
            bw.Close();
            fs.Close();
        }

        //Closes all streams, files and libraries
        public void Close()
        {
            if (getFrameObject != 0)
            {
                AviReadingMethods.AVIStreamGetFrameClose(getFrameObject);
                getFrameObject = 0;
            }
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
    }
}
