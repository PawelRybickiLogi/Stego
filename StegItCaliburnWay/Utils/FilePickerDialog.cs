using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Win32;
using StegItCaliburnWay.Utils;
using Type = StegItCaliburnWay.Utils.Type;

namespace StegItCaliburnWay
{
    public class FilePickerDialog
    {
        public byte[] OpenReadDialog(Type dialogType)
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = dialogType.defaultExt,
                Filter = dialogType.filter
            };

            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                throw new ArgumentException();
            }

            var bytes = FileReader.ReadFile(dlg.FileName);

            //var chars = TextUtils.GetUTF8CharArrayFromByteStream(bytes);

            return bytes;
        }

        public ImageFile OpenReadImageDialog(Type fileType)
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = fileType.defaultExt,
                Filter = fileType.filter
            };

            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                throw new ArgumentException();
            }

            var bitmap = new Bitmap(Image.FromFile(dlg.FileName, true));
            var bytes = FileReader.ReadFile(dlg.FileName);

            //var px1 = bitmap.GetPixel(0, 0);
            var px2 = bitmap.GetPixel(1, 0);

            return new ImageFile(bitmap, bytes);
        }

        public void OpenSaveImageDialog(Bitmap bitmapImage, Type fileType)
        {
            var dlg = new SaveFileDialog
            {
                DefaultExt = fileType.defaultExt,
                Filter = fileType.filter
            };

            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                throw new ArgumentException();
            }

            //if (bitmapImage.Is32BitImage())
            bitmapImage.Save(dlg.FileName);
            //else
            //    bitmapImage.SaveAsNot32BitImage(dlg.FileName);
            //File.WriteAllBytes(dlg.FileName, ImageUtils.BitmapToBytes(bitmapImage));
        }

        public void OpenSaveDialog(Type dialogType, byte[] hiddenMessage)
        {
            var dlg = new SaveFileDialog
            {
                DefaultExt = dialogType.defaultExt,
                Filter = dialogType.filter
            };
            
            dlg.ShowDialog();

            if (dlg.FileName == "") 
                return;

            FileWriter.WriteToFile(dlg.FileName, hiddenMessage);
        }

    }
}
