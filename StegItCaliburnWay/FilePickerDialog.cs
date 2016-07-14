using System;
using Microsoft.Win32;

namespace StegItCaliburnWay
{
    public class FilePickerDialog
    {
        public char[] OpenReadDialog()
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

            var chars = TextUtils.GetUTF8CharArrayFromByteStream(bytes);

            return chars;
        }

        public void OpenSaveDialog(char[] hiddenMessage)
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
