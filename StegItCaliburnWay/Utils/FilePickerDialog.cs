using System;
using Microsoft.Win32;

namespace StegItCaliburnWay
{
    public class FilePickerDialog
    {
        public byte[] OpenReadDialog()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter =
                    "TXT Files (*.txt)|*.txt|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
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
