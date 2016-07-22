using System;
using System.Drawing;
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

        public ImageFilePicked OpenReadImageDialog()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = DialogType.Image.defaultExt,
                Filter = DialogType.Image.filter
            };

            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                throw new ArgumentException();
            }

            var bitmap = (Bitmap) Image.FromFile(dlg.FileName, true);
            var bytes = FileReader.ReadFile(dlg.FileName);

            return new ImageFilePicked(bitmap, bytes);

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
