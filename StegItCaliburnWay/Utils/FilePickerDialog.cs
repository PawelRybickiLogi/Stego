using System;
using Microsoft.Win32;
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

        public void OpenSaveDialog(byte[] hiddenMessage)
        {
            var dlg = new SaveFileDialog
            {
                DefaultExt = ".txt",
                Filter = "TXT Files (*.txt)|*.txt"
            };
            
            dlg.ShowDialog();

            if (dlg.FileName == "") 
                return;

            FileWriter.WriteToFile(dlg.FileName, hiddenMessage);
        }
    }
}
