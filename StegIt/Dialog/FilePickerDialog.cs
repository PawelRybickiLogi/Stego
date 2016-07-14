using Microsoft.Win32;
using StegIt.Text;
using StegIt.Text.StegoTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StegIt.WorkContext;

namespace StegIt.Dialog
{
    class FilePickerDialog
    {
        public static char[] OpenReadDialog()
        {
            var dlg = new OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "TXT Files (*.txt)|*.txt|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"; 

            dlg.ShowDialog();

            if (dlg.FileName == "")
                return null;

            var bytes = FileReader.ReadFile(dlg.FileName);

            var chars = TextUtils.GetUTF8CharArrayFromByteStream(bytes);

            return chars;
        }

        public static void OpenSaveDialog(char[] hiddenMessage)
        {
            var dlg = new SaveFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "TXT Files (*.txt)|*.txt"; 

            dlg.ShowDialog();

            if (dlg.FileName == "") 
                return;

            FileWriter.WriteToFile(dlg.FileName, hiddenMessage);
        }
    }
}
